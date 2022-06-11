using System;
using TAKACHIYO.ActorControllers;
using UniRx;
using UnityEngine;

namespace TAKACHIYO.CommandSystems.Conditions
{
    /// <summary>
    /// ダメージをn回受けたら実行可能
    /// </summary>
    public sealed class TakeDamageCount : CommandCondition
    {
        [SerializeField]
        private int number = 1;

        public override IObservable<Unit> TryCastAsObservable(Actor owner)
        {
            return Observable.Defer(() =>
            {
                return owner.Broker.Receive<ActorEvent.TakedDamage>().AsUnitObservable();
            });
        }
        
        public override bool Evaluate(Command command)
        {
            return (command.Owner.StatusController.TakeDamageCount - command.LastTakeDamageCount) >= this.number;
        }
    }
}
