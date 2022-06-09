using System;
using System.Collections.Generic;
using System.Linq;
using TAKACHIYO.ActorControllers;
using UniRx;

namespace TAKACHIYO.CommandSystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Command
    {
        private readonly CommandBlueprint blueprint;

        private readonly ReactiveProperty<float> currentCastTime;

        private Actor owner;

        private List<IObservable<Unit>> commandInvokeStreams;

        public string CommandName => this.blueprint.CommandName;
        
        public bool CanInvoke => this.currentCastTime.Value >= this.blueprint.CastTime;

        public float CastTimeRate => this.currentCastTime.Value / this.blueprint.CastTime;

        public IReadOnlyReactiveProperty<float> CurrentCastTime => this.currentCastTime;

        /// <summary>
        /// 詠唱を開始できるか返す
        /// </summary>
        public bool CanCasting => this.blueprint.Condition.Evaluate(this.owner.CommandController, this);

        public Command(CommandBlueprint blueprint, Actor owner)
        {
            this.blueprint = blueprint;
            this.owner = owner;
            this.currentCastTime = new ReactiveProperty<float>();
            this.commandInvokeStreams = this.blueprint.Actions.Select(x => x.Invoke(this.owner)).ToList();
        }

        public void Reset()
        {
            this.currentCastTime.Value = 0.0f;
        }

        public void Update(float deltaTime)
        {
            this.currentCastTime.Value += deltaTime;
        }

        public IObservable<Unit> Invoke()
        {
            return Observable.Defer(() => this.commandInvokeStreams.Concat().AsSingleUnitObservable());
        }
    }
}
