using System;
using System.Collections.Generic;
using TAKACHIYO.CommandSystems;
using TAKACHIYO.MasterDataSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.ActorControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Actor
    {
        public ActorStatusController StatusController { get; }
        
        public ActorCommandController CommandController { get; }
        
        public Actor(ActorSetupData setupData)
        {
            this.StatusController = new ActorStatusController(setupData.masterDataActorStatusId);
            this.CommandController = new ActorCommandController(setupData.commandBlueprintIds);
        }

        public IObservable<Unit> SetupAsync()
        {
            return Observable.WhenAll(
                this.CommandController.SetupAsync()
                );
        }
    }
}
