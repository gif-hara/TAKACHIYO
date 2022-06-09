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
        
        /// <summary>
        /// 対戦相手
        /// </summary>
        public Actor Opponent { get; private set; }
        
        public Actor(ActorSetupData setupData)
        {
            this.StatusController = new ActorStatusController(setupData.masterDataActorStatusId);
            this.CommandController = new ActorCommandController(setupData.commandBlueprintIds);
        }

        public IObservable<Unit> SetupAsync(Actor opponent)
        {
            this.Opponent = opponent;
            return Observable.WhenAll(
                this.CommandController.SetupAsync()
                );
        }
    }
}
