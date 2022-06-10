using System;
using UniRx;
using UnityEngine;

namespace TAKACHIYO.CommandSystems.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CommandAction : ICommandAction
    {
        [SerializeField]
        protected Define.TargetType targetType;

        public abstract IObservable<Unit> Invoke(Command command);
    }
}
