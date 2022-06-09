using System;
using TAKACHIYO.ActorControllers;
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
        
        public override IObservable<Unit> Invoke(Actor owner)
        {
            return Observable.Defer(() =>
            {
                foreach (var target in owner.GetTargets(this.targetType))
                {
                    target.StatusController.TakeDamage(Mathf.FloorToInt(10 * this.rate));
                }

                return Observable.ReturnUnit();
            });
        }
    }
}
