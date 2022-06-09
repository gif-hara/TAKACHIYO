using System;
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
        
        public Actor(ActorSetupData setupData)
        {
            this.StatusController = new ActorStatusController(setupData.masterDataActorStatusId);
            this.CommandController = new ActorCommandController(this, setupData.commandBlueprintIds);
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
