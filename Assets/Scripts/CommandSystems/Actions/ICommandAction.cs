using System;
using TAKACHIYO.ActorControllers;
using UniRx;

namespace TAKACHIYO.CommandSystems.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommandAction
    {
        IObservable<Unit> Invoke(Actor owner);
    }
}
