﻿using Master40.Models.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Master40.Models;

namespace Master40.Data
{
    public static class MasterDBInitializer
    {
        public static void DbInitialize(MasterDBContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Entrys.
            if (context.Articles.Any())
            {
                return;   // DB has been seeded
            }
            // Article Types
            var articleTypes = new ArticleType[]
            {
                new ArticleType {Name = "Assembly"},
                new ArticleType {Name = "Material"},
                new ArticleType {Name = "Consumable"}
            };
            foreach (ArticleType at in articleTypes)
            {
                context.ArticleTypes.Add(at);
            }
            context.SaveChanges();

            // Units
            var units = new Unit[]
            {
                new Unit {Name = "Kilo"},
                new Unit {Name = "Litre"},
                new Unit {Name = "Pieces"}
            };
            foreach (Unit u in units)
            {
                context.Units.Add(u);
            }
            context.SaveChanges();



            var machines = new Machine[] {
                new Machine{Capacity=1, Name="Säge", Count = 1, MachineGroup = new MachineGroup{ Name = "Zuschnitt" } },
                new Machine{Capacity=1, Name="Bohrer", Count = 1, MachineGroup = new MachineGroup{ Name = "Bohrwerk" } },
                new Machine{Capacity=1, Name="MontagePlatform", Count=1, MachineGroup = new MachineGroup{ Name = "Montage" }}
            };
            foreach (var m in machines)
            {
                context.Machines.Add(m);
            }
            context.SaveChanges();

            var machineTools = new MachineTool[]
            {
                new MachineTool{MachineId=machines.Single(m => m.Name == "Säge").MachineId, SetupTime=1, Name="Sägeblatt 1mm Zahnabstant"},
                new MachineTool{MachineId=machines.Single(m => m.Name == "Bohrer").MachineId, SetupTime=1, Name="M6 Bohrkopf"},
            };
            foreach (var mt in machineTools)
            {
                context.MachineTools.Add(mt);
            }
            context.SaveChanges();


            // Articles
            var articles = new Article[]
            {
            new Article{Name="Kipper",  ArticleTypeId = articleTypes.Single( s => s.Name == "Assembly").ArticleTypeId, CreationDate = DateTime.Parse("2016-09-01"), DeliveryPeriod = 20, UnitId = units.Single( s => s.Name == "Pieces").UnitId, Price = 45.00},
            new Article{Name="Rahmengestell",ArticleTypeId = articleTypes.Single( s => s.Name == "Assembly").ArticleTypeId, DeliveryPeriod = 10, UnitId = units.Single( s => s.Name == "Pieces").UnitId, Price = 15.00},
            new Article{Name="Ladebehälter", ArticleTypeId = articleTypes.Single( s => s.Name == "Assembly").ArticleTypeId, CreationDate = DateTime.Parse("2016-09-01"), DeliveryPeriod = 10, UnitId = units.Single( s => s.Name == "Pieces").UnitId, Price = 15.00},
            new Article{Name="Chassis", ArticleTypeId = articleTypes.Single( s => s.Name == "Assembly").ArticleTypeId, CreationDate = DateTime.Parse("2016-09-01"), DeliveryPeriod = 10, UnitId = units.Single( s => s.Name == "Pieces").UnitId, Price = 15.00},

            new Article{Name="Rad", ArticleTypeId = articleTypes.Single( s => s.Name == "Material").ArticleTypeId, CreationDate = DateTime.Parse("2002-09-01"), DeliveryPeriod = 10, UnitId = units.Single( s => s.Name == "Pieces").UnitId, Price = 1.00},
            new Article{Name="Felge", ArticleTypeId = articleTypes.Single( s => s.Name == "Material").ArticleTypeId, CreationDate = DateTime.Parse("2002-09-01"), DeliveryPeriod = 2, UnitId = units.Single( s => s.Name == "Pieces").UnitId, Price = 1.00},
            new Article{Name="Bodenplatte", ArticleTypeId = articleTypes.Single( s => s.Name == "Material").ArticleTypeId, CreationDate = DateTime.Parse("2002-09-01"), DeliveryPeriod = 10, UnitId = units.Single( s => s.Name == "Pieces").UnitId, Price = 4.00},
            new Article{Name="Aufliegeplatte", ArticleTypeId = articleTypes.Single( s => s.Name == "Material").ArticleTypeId, CreationDate = DateTime.Parse("2002-09-01"), DeliveryPeriod = 10, UnitId = units.Single( s => s.Name == "Pieces").UnitId, Price = 3.00},
            
            new Article{Name="Seitenwand land", ArticleTypeId = articleTypes.Single( s => s.Name == "Material").ArticleTypeId, CreationDate = DateTime.Parse("2002-09-01"), DeliveryPeriod = 10, UnitId = units.Single( s => s.Name == "Pieces").UnitId, Price = 1.50},
            new Article{Name="Seitenwand kurz", ArticleTypeId = articleTypes.Single( s => s.Name == "Material").ArticleTypeId, CreationDate = DateTime.Parse("2002-09-01"), DeliveryPeriod = 10, UnitId = units.Single( s => s.Name == "Pieces").UnitId, Price = 2.00},
            new Article{Name="Bodenplatte Behälter", ArticleTypeId = articleTypes.Single( s => s.Name == "Material").ArticleTypeId, CreationDate = DateTime.Parse("2002-09-01"), DeliveryPeriod = 10, UnitId = units.Single( s => s.Name == "Pieces").UnitId, Price = 4.00},
            new Article{Name="Fahrerhaus", ArticleTypeId = articleTypes.Single( s => s.Name == "Material").ArticleTypeId, CreationDate = DateTime.Parse("2002-09-01"), DeliveryPeriod = 10, UnitId = units.Single( s => s.Name == "Pieces").UnitId, Price = 5.00},
            new Article{Name="Motorblock", ArticleTypeId = articleTypes.Single( s => s.Name == "Material").ArticleTypeId, CreationDate = DateTime.Parse("2002-09-01"), DeliveryPeriod = 10, UnitId = units.Single( s => s.Name == "Pieces").UnitId, Price = 3.00},
            new Article{Name="Achse", ArticleTypeId = articleTypes.Single( s => s.Name == "Material").ArticleTypeId, CreationDate = DateTime.Parse("2002-09-01"), DeliveryPeriod = 10, UnitId = units.Single( s => s.Name == "Pieces").UnitId, Price = 0.50},
            new Article{Name="Knopf", ArticleTypeId = articleTypes.Single( s => s.Name == "Material").ArticleTypeId, CreationDate = DateTime.Parse("2002-09-01"), DeliveryPeriod = 10, UnitId = units.Single( s => s.Name == "Kilo").UnitId, Price = 0.05},
            new Article{Name="Kippgelenk", ArticleTypeId = articleTypes.Single( s => s.Name == "Material").ArticleTypeId, CreationDate = DateTime.Parse("2002-09-01"), DeliveryPeriod = 10, UnitId = units.Single( s => s.Name == "Pieces").UnitId, Price = 1},
            new Article{Name="Holz 1,5m x 3,0m", ArticleTypeId = articleTypes.Single( s => s.Name == "Material").ArticleTypeId, CreationDate = DateTime.Parse("2002-09-01"), DeliveryPeriod = 5, UnitId = units.Single( s => s.Name == "Pieces").UnitId, Price = 5},

            new Article{Name="Unterlegscheibe", ArticleTypeId = articleTypes.Single( s => s.Name == "Consumable").ArticleTypeId, CreationDate = DateTime.Parse("2002-09-01"), DeliveryPeriod = 10,UnitId = units.Single( s => s.Name == "Kilo").UnitId, Price = 0.05},
            new Article{Name="Leim", ArticleTypeId = articleTypes.Single( s => s.Name == "Consumable").ArticleTypeId, CreationDate = DateTime.Parse("2002-09-01"), DeliveryPeriod = 10, UnitId = units.Single( s => s.Name == "Kilo").UnitId, Price = 5.00},
            new Article{Name="Dübel", ArticleTypeId = articleTypes.Single( s => s.Name == "Consumable").ArticleTypeId, CreationDate = DateTime.Parse("2005-09-01"), DeliveryPeriod = 3, UnitId = units.Single( s => s.Name == "Kilo").UnitId, Price = 3.00},
            new Article{Name="Verpackung", ArticleTypeId = articleTypes.Single( s => s.Name == "Consumable").ArticleTypeId, CreationDate = DateTime.Parse("2005-09-01"), DeliveryPeriod  = 4, UnitId = units.Single( s => s.Name == "Kilo").UnitId, Price = 7.00},
            new Article{Name="Bedienungsanleitung", ArticleTypeId = articleTypes.Single( s => s.Name == "Consumable").ArticleTypeId, CreationDate = DateTime.Parse("2005-09-01"), DeliveryPeriod  = 4, UnitId = units.Single( s => s.Name == "Kilo").UnitId, Price = 0.50},

            };
            foreach (Article a in articles)
            {
                context.Articles.Add(a);
            }
            context.SaveChanges();

            // get the name -> id mappings
            var DBArticles = context
              .Articles
              .ToDictionary(p => p.Name, p => p.ArticleId);


            // create Stock Entrys for each Article
            foreach (var article in DBArticles)
            {
                var Stocks = new Stock[]
                {
                    new Stock
                    {
                        ArticleForeignKey = article.Value,
                        Name = "Stock: " + article.Key,
                        Min = 0,
                        Max = 50,
                        Current = 0
                    }
                };
                foreach (Stock s in Stocks)
                {
                    context.Stocks.Add(s);
                }
                context.SaveChanges();
            }
            var articleBom = new List<ArticleBom>
            {
                new ArticleBom { ArticleId = articles.Single(a => a.Name == "Kipper").ArticleId, Name = "Kipper", ArticleBomItems = new List<ArticleBomItem> {
                    new ArticleBomItem { ArticleId =  articles.Single(a => a.Name == "Rahmengestell").ArticleId, Name="Rahmengestell", Quantity = 1 },
                    new ArticleBomItem { ArticleId =  articles.Single(a => a.Name == "Ladebehälter").ArticleId, Name="Ladebehälter", Quantity = 1 },
                    new ArticleBomItem { ArticleId =  articles.Single(a => a.Name == "Chassis").ArticleId, Name="Chassis", Quantity = 1 }
                    }
                },
                new ArticleBom { ArticleId = articles.Single(a => a.Name == "Rahmengestell").ArticleId, Name = "Rahmengestell", ArticleBomItems = new List<ArticleBomItem> {
                    new ArticleBomItem { ArticleId =  articles.Single(a => a.Name == "Bodenplatte").ArticleId, Name="Bodenplatte", Quantity = 1 },
                    new ArticleBomItem { ArticleId =  articles.Single(a => a.Name == "Achse").ArticleId, Name="Achse", Quantity = 2 },
                    new ArticleBomItem { ArticleId =  articles.Single(a => a.Name == "Rad").ArticleId, Name="Rad", Quantity = 4 }
                    }
                },
                new ArticleBom { ArticleId = articles.Single(a => a.Name == "Rad").ArticleId, Name = "Rad", ArticleBomItems = new List<ArticleBomItem> {
                    new ArticleBomItem { ArticleId =  articles.Single(a => a.Name == "Felge").ArticleId, Name="Felge", Quantity = 1 },
                    new ArticleBomItem { ArticleId =  articles.Single(a => a.Name == "Dübel").ArticleId, Name="Dübel", Quantity = 1 },
                 }
                }
            };
            foreach (var item in articleBom)
            {
                context.ArticleBoms.Add(item);
            }
            context.SaveChanges();





            var menu = new Menu();
            var menuItems = new List<MenuItem>{
                new MenuItem{MenuText = "Article", LinkUrl = "#", MenuOrder = 1, Action="Index", Symbol="fa-th-list"},
                new MenuItem{MenuText = "Order", LinkUrl = "Orders", MenuOrder = 2, Action="Index", Symbol="fa-archive"},
                new MenuItem{MenuText = "Purchase", LinkUrl = "Purchases", MenuOrder = 3, Action="Index", Symbol="fa-shopping-cart"},
                new MenuItem{MenuText = "Business Partner", LinkUrl = "BusinessPartners", MenuOrder = 4, Action="Index", Symbol="fa-group"},
                new MenuItem{MenuText = "Article", LinkUrl = "Articles", MenuOrder = 1, ParentMenuItemId = 1, Action="Index", Symbol="fa-archive"},
                new MenuItem{MenuText = "Operations", LinkUrl = "#", MenuOrder = 2, ParentMenuItemId = 1, Action="Index", Symbol="fa-th-list"},
                new MenuItem{MenuText = "Article BOM", LinkUrl = "ArticleBoms", MenuOrder = 2, ParentMenuItemId = 1, Action="Index", Symbol="fa-sitemap"},
                new MenuItem{MenuText = "Article Stock", LinkUrl = "Stocks",  MenuOrder = 1, ParentMenuItemId = 1,  Action="Index", Symbol="fa-dropbox"},
                new MenuItem{MenuText = "Operation Chart", LinkUrl = "OperationCharts",  MenuOrder = 1, ParentMenuItemId = 6, Action="Index", Symbol="fa-tasks"},
                new MenuItem{MenuText = "Operation Tools", LinkUrl = "OperationTools",  MenuOrder = 1, ParentMenuItemId = 6, Action="Index", Symbol="fa-wrench"},
                new MenuItem{MenuText = "Operation Machine", LinkUrl = "OperationMachines",  MenuOrder = 1, ParentMenuItemId = 6, Action="Index", Symbol="fa-gears"},
                new MenuItem{MenuText = "Planung Simulations", LinkUrl = "#",  MenuOrder = 4, Action="Index", Symbol="fa-spinner"},
                new MenuItem{MenuText = "MRP", LinkUrl = "Mrp",  MenuOrder = 5, ParentMenuItemId = 12, Action="Index", Symbol="fa-magic"},
            };
            menu.MenuItems = menuItems;
            menu.MenuName = "Master 4.0";

            context.Menus.Add(menu);
            context.SaveChanges();

            //create Businesspartner
            var businessPartner = new BusinessPartner(){Debitor = true,Kreditor = true,Name = "Toys'R'us Spielwarenabteilung"};
            context.BusinessPartners.Add(businessPartner);
            context.SaveChanges();

            //create order
            var orders = new List<Order>() { 
                new Order(){BusinessPartnerId = businessPartner.BusinessPartnerId, DueTime = 10, Name = "Kipperbestellung"},
                new Order() { BusinessPartnerId = businessPartner.BusinessPartnerId, DueTime = 9, Name = "Kipperbestellung" },
                new Order() { BusinessPartnerId = businessPartner.BusinessPartnerId, DueTime = 8, Name = "Kipperbestellung" }
            };
            foreach (var order in orders)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();

            //create orderParts
            var orderParts = new List<OrderPart>()
            {
                new OrderPart(){Quantity = 5, ArticleId = articles.Single(a => a.Name == "Kipper").ArticleId, OrderId = 1},
                new OrderPart(){Quantity = 6, ArticleId = articles.Single(a => a.Name == "Kipper").ArticleId, OrderId = 2},
                new OrderPart(){Quantity = 7, ArticleId = articles.Single(a => a.Name == "Kipper").ArticleId, OrderId = 3},
            };
            foreach (var orderPart in orderParts)
            {
                context.OrderParts.Add(orderPart);
            }
            context.SaveChanges();


        }
    }
}
