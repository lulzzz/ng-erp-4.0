﻿using System;

namespace Master40.Agents.Agents.Model
{
    public class Proposal
    {
        public int PossibleSchedule { get; set; }
        public Guid AgentId { get; set; }
        public Guid WorkItemId { get; set; }
    }
}