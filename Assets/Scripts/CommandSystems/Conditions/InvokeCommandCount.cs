using System;
using TAKACHIYO.ActorControllers;
using TAKACHIYO.BattleSystems;
using UniRx;
using UnityEngine;

namespace TAKACHIYO.CommandSystems.Conditions
{
    /// <summary>
    /// n回だけ実行可能
    /// </summary>
    public sealed class InvokeCommandCount : CommandCondition
    {
        [SerializeField]
        private int number = 1;

        public override IObservable<Unit> TryCastAsObservable(Actor owner)
        {
            return Observable.Defer(() =>
            {
                return Observable.Merge(
                    BattleController.Broker.Receive<BattleEvent.StartBattle>().AsUnitObservable(),
                    owner.Broker.Receive<ActorEvent.InvokedCommand>().AsUnitObservable()
                    );
            });
        }
        
        public override bool Evaluate(Command command)
        {
            if (this.number <= 0)
            {
                return true;
            }
            
            return command.InvokedCount < this.number;
        }
    }
}
