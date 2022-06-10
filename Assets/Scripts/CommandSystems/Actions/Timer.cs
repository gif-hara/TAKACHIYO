using System;
using TAKACHIYO.ActorControllers;
using UniRx;
using UnityEngine;

namespace TAKACHIYO.CommandSystems.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Timer : CommandAction
    {
        [SerializeField]
        private float delaySeconds;
        
        public override IObservable<Unit> Invoke(Command command)
        {
            return Observable.Defer(() =>
            {
                return Observable.Timer(TimeSpan.FromSeconds(this.delaySeconds))
                    .AsUnitObservable();
            });
        }
    }
}
