﻿using System;
using System.Collections.Generic;
using System.Linq;
using Master40.Agents.Agents.Internal;
using Master40.Agents.Agents.Model;
using Master40.DB.Data.Context;
using Master40.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ResultOperators.Internal;

namespace Master40.Agents.Agents
{
    public class SystemAgent : Agent
    {
        private readonly ProductionDomainContext _productionDomainContext;

        public SystemAgent(Agent creator, string name, bool debug, ProductionDomainContext productionDomainContext) : base(creator, name, debug)
        {
            this._productionDomainContext = productionDomainContext;
        }
        public enum InstuctionsMethods
        {
            CreateContractAgent,
            RequestArticleBom,
        }
        //TODO: System Talk.

        private void CreateContractAgent(InstructionSet instructionSet)
        {
            //  Check 0 ref
            var requestItem = instructionSet.ObjectToProcess as OrderPart;
            if (requestItem == null)
            {
                throw new InvalidCastException(this.Name + " Cast to OrderPart Failed");
            }

            // Create DemandRequester
            var demand = _productionDomainContext.CreateDemandOrderPart(requestItem);
            requestItem.DemandOrderParts = new List<DemandOrderPart>{ (DemandOrderPart)demand };

            // Create Agent
            CreateAgents(requestItem);
        }

        private void RequestArticleBom(InstructionSet instructionSet)
        {
            //  Check 0 ref
            var requestItem = instructionSet.ObjectToProcess as RequestItem;
            if (requestItem == null)
            {
                throw new InvalidCastException(this.Name + " Cast to RequestItem Failed");
            }

            // debug
            DebugMessage(" Request details for article: " + requestItem.Article.Name);

            // get BOM from Context
            var article = _productionDomainContext.Articles
                                                    .Include(x => x.WorkSchedules)
                                                        .ThenInclude(x => x.MachineGroup)
                                                    .Include(x => x.ArticleBoms)
                                                        .ThenInclude(x => x.ArticleChild)
                                                    .SingleOrDefault(x => x.Id == requestItem.Article.Id);
            // calback with po.bom
            CreateAndEnqueueInstuction(methodName: DispoAgent.InstuctionsMethods.ResponseFromSystemForBom.ToString(),
                                  objectToProcess: article,
                                      targetAgent: instructionSet.SourceAgent);

        }


        private void CreateAgents(OrderPart contract)
        {
            // Create Contract agents
            var ca = new ContractAgent(creator: this,
                name: contract.Order.Name + " - Part:" + contract.Article.Name,
                debug: true);
            // add To System
            this.ChildAgents.Add(ca);

            // enqueue Order
            CreateAndEnqueueInstuction(methodName: ContractAgent.InstuctionsMethods.StartOrder.ToString(),
                                  objectToProcess: contract,
                                      targetAgent: ca);

        }

        public void PrepareAgents()
        {
            foreach (var orderpart in _productionDomainContext.OrderParts
                                                                .Include(x => x.Article)
                                                                .Include(x => x.Order)
                                                                .AsNoTracking())
            {
                this.InstructionQueue.Enqueue(new InstructionSet
                {
                    MethodName = SystemAgent.InstuctionsMethods.CreateContractAgent.ToString(),
                    ObjectToProcess = orderpart,
                    ObjectType = orderpart.GetType(),
                    SourceAgent = this,
                });
            }
        }

    }
}