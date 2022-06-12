using System;
using System.Collections.Generic;
using System.Linq;
using TAKACHIYO.ActorControllers;
using TAKACHIYO.BattleSystems;
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

        public readonly ICommandBlueprintHolder BlueprintHolder;

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
        private bool CanCasting => this.blueprint.Conditions.All(condition => condition.Evaluate(this));
        
        /// <summary>
        /// 最後に実行した時のコマンド実行回数
        /// </summary>
        public int LastInvokedOrder { get; private set; }
        
        /// <summary>
        /// 最後に実行した時のダメージを受けた回数
        /// </summary>
        public int LastTakeDamageCount { get; private set; }
        
        /// <summary>
        /// 実行した回数
        /// </summary>
        public int InvokedCount { get; private set; }
        
        /// <summary>
        /// 詠唱中か返す
        /// </summary>
        public bool IsCasting { get; set; }

        public Command(CommandBlueprint blueprint, ICommandBlueprintHolder blueprintHolder, Actor owner)
        {
            this.blueprint = blueprint;
            this.BlueprintHolder = blueprintHolder;
            this.Owner = owner;
            this.currentCastTime = new ReactiveProperty<float>();
            this.commandInvokeStreams = this.blueprint.Actions.Select(x => x.Invoke(this)).ToList();
            this.blueprint.Conditions.Select(x => x.TryCastAsObservable(this.Owner))
                .Merge()
                .TakeUntil(BattleController.Broker.Receive<BattleEvent.EndBattle>())
                .Subscribe(_ =>
                {
                    if (this.IsCasting || !this.CanCasting)
                    {
                        return;
                    }
                    
                    this.IsCasting = true;
                    this.Owner.CommandController.AddCastingCommand(this);
                });
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
                this.InvokedCount++;
                this.LastInvokedOrder = this.Owner.CommandController.TotalInvokedCount;
                this.IsCasting = false;

                return this.commandInvokeStreams
                    .Concat()
                    .AsSingleUnitObservable()
                    .Do(_ =>
                    {
                        this.LastTakeDamageCount = this.Owner.StatusController.TakeDamageCount;
                    });
            });
        }
    }
}
