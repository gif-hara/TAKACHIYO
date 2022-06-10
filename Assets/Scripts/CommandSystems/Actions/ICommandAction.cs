using System;
using UniRx;

namespace TAKACHIYO.CommandSystems.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommandAction
    {
        IObservable<Unit> Invoke(Command command);
    }
}
