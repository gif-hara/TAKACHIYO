using System;
using TAKACHIYO.ActorControllers;
using UniRx;
using UnityEngine;

namespace TAKACHIYO.CommandSystems.Conditions
{
    /// <summary>
    /// 何かコマンドがn回実行したら実行可能
    /// </summary>
    public sealed class InvokeCommandCountAny : CommandCondition
    {
        [SerializeField]
        private int number = 1;

        public override IObservable<Unit> TryCastAsObservable(Actor owner)
        {
            return Observable.Defer(() =>
            {
                return owner.Broker.Receive<ActorEvent.InvokedCommand>().AsUnitObservable();
            });
        }
        public override bool Evaluate(Command command)
        {
            return (command.Owner.CommandController.TotalInvokedCount - command.LastInvokedOrder) >= this.number;
        }
    }
}
