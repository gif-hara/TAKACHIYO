using System;
using TAKACHIYO.ActorControllers;
using UniRx;

namespace TAKACHIYO.CommandSystems.CommandConditions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CommandCondition : ICommandCondition
    {
        public abstract IObservable<Unit> TryCastAsObservable(Actor owner);
        
        public abstract bool Evaluate(Command command);
        
        public abstract string LocalizedDescription { get; }
    }
}
