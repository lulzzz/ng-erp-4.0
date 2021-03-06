﻿module FCentralActivities

open Akka.Actor

    type public FCentralActivity = {
        ResourceId : int
        ProductionOrderId: string
        OperationId: string
        ActivityId: int
        Start: int64
        Duration: int64
        Name: string
        GanttPlanningInterval: int
        Hub: IActorRef
        Capability : string        
        ActivityType : string
    } with
        member this.Key with get() = this.ProductionOrderId + "|" + this.OperationId + "|" + this.ActivityId.ToString()

        