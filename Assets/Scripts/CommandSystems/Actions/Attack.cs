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
        
        public override IObservable<Unit> Invoke(Command command)
        {
            return Observable.Defer(() =>
            {
                foreach (var target in command.Owner.GetTargets(this.targetType))
                {
                    var damage = Calcurator.GetDamage(command.Owner, target, command, this.rate);

                    target.StatusController.TakeDamage(
                        command.Owner,
                        damage,
                        true
                        );
                }

                return Observable.ReturnUnit();
            });
        }
    }
}
