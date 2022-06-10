using System;
using TAKACHIYO.ActorControllers;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.CommandSystems.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Log : CommandAction
    {
        [SerializeField]
        private string message;
        
        public override IObservable<Unit> Invoke(Command command)
        {
            return Observable.Defer(() =>
            {
                Debug.Log(this.message);
                
                return Observable.ReturnUnit();
            });
        }
    }
}
