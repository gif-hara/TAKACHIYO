using System;
using System.Collections.Generic;
using System.Linq;
using TAKACHIYO.ActorControllers;
using UniRx;
using UnityEngine;

namespace TAKACHIYO.CommandSystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Command
    {
        private readonly CommandBlueprint blueprint;

        private readonly ReactiveProperty<float> currentCastTime;

        public Actor Owner { get; }

        private List<IObservable<Unit>> commandInvokeStreams;

        public string CommandName => this.blueprint.CommandName;
        
        public bool CanInvoke => this.currentCastTime.Value >= this.blueprint.CastTime;

        public float CastTimeRate => this.currentCastTime.Value / this.blueprint.CastTime;

        public IReadOnlyReactiveProperty<float> CurrentCastTime => this.currentCastTime;

        /// <summary>
        /// 詠唱を開始できるか返す
        /// </summary>
        public bool CanCasting => this.blueprint.Condition.Evaluate(this);
        
        /// <summary>
        /// 最後に攻撃を行った順番
        /// </summary>
        public int LastInvokeOrder { get; private set; }

        public Command(CommandBlueprint blueprint, Actor owner)
        {
            this.blueprint = blueprint;
            this.Owner = owner;
            this.currentCastTime = new ReactiveProperty<float>();
            this.commandInvokeStreams = this.blueprint.Actions.Select(x => x.Invoke(this.Owner)).ToList();
        }

        public void Reset()
        {
            this.currentCastTime.Value = 0.0f;
        }

        public void Update(float deltaTime)
        {
            var result = this.currentCastTime.Value + deltaTime;
            result = Mathf.Min(result, this.blueprint.CastTime);
            this.currentCastTime.Value = result;
        }

        public IObservable<Unit> Invoke()
        {
            return Observable.Defer(() =>
            {
                return this.commandInvokeStreams
                    .Concat()
                    .AsSingleUnitObservable()
                    .Do(_ =>
                    {
                        this.LastInvokeOrder = this.Owner.CommandController.InvokedCount + 1;
                    });
            });
        }
    }
}
