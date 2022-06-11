using System;
using UniRx;
using UnityEngine;

namespace TAKACHIYO.CommandSystems.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Recovery : CommandAction
    {
        [SerializeField]
        private float rate = 1.0f;
        
        public override IObservable<Unit> Invoke(Command command)
        {
            return Observable.Defer(() =>
            {
                foreach (var target in command.Owner.GetTargets(this.targetType))
                {
                    var damage = Mathf.FloorToInt(command.BlueprintHolder.RecoveryPower * this.rate);
                    
                    target.StatusController.TakeDamageRaw(-damage, true);
                }

                return Observable.ReturnUnit();
            });
        }
    }
}
