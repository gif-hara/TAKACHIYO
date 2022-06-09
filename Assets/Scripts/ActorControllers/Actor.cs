using System;
using Cysharp.Threading.Tasks;
using UniRx;

namespace TAKACHIYO.ActorControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Actor
    {
        public ActorStatusController StatusController { get; }
        
        public ActorCommandController CommandController { get; }

        private readonly MessageBroker broker = new MessageBroker();

        public IMessageBroker Broker => this.broker;
        
        /// <summary>
        /// 対戦相手
        /// </summary>
        public Actor Opponent { get; private set; }
        
        public Actor(IActorSetupData setupData)
        {
            this.StatusController = new ActorStatusController(this, setupData.MasterDataActorStatusId);
            this.CommandController = new ActorCommandController(this, setupData.CommandBlueprintSetupData);
        }

        public UniTask SetupAsync(Actor opponent)
        {
            this.Opponent = opponent;
            
            return this.CommandController.SetupAsync();
        }
    }
}
