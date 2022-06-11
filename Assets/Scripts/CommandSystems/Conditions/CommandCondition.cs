using System;
using TAKACHIYO.ActorControllers;
using UniRx;

namespace TAKACHIYO.CommandSystems.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CommandCondition : ICommandCondition
    {
        public abstract IObservable<Unit> TryCastAsObservable(Actor owner);
        
        public abstract bool Evaluate(Command command);
    }
}
