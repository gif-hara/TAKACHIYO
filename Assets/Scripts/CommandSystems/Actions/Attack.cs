using System;
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
                    target.StatusController.TakeDamage(Mathf.FloorToInt(command.BlueprintHolder.Strength * this.rate));
                }

                return Observable.ReturnUnit();
            });
        }
    }
}
