using System;
using TAKACHIYO.BattleSystems;
using UniRx;
using UnityEngine;

namespace TAKACHIYO.CommandSystems.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Attack : CommandAction
    {
        [SerializeField]
        private float rate = 1.0f;

        [SerializeField]
        private Define.AttackType attackType;
        
        public override IObservable<Unit> Invoke(Command command)
        {
            return Observable.Defer(() =>
            {
                foreach (var target in command.Owner.GetTargets(this.targetType))
                {
                    var damage = Calcurator.GetAttackDamage(command.Owner, target, command, this.rate, this.attackType);

                    target.StatusController.TakeDamage(command.Owner, damage);
                }

                return Observable.ReturnUnit();
            });
        }
    }
}
