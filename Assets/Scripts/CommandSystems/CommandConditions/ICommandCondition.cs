using System;
using TAKACHIYO.ActorControllers;
using UniRx;

namespace TAKACHIYO.CommandSystems.CommandConditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommandCondition
    {
        /// <summary>
        /// 詠唱可能か試すストリームを返す
        /// </summary>
        IObservable<Unit> TryCastAsObservable(Actor owner);
        
        bool Evaluate(Command command);
        
        /// <summary>
        /// ローカライズ済みの説明文を返す
        /// </summary>
        string LocalizedDescription { get; }
    }
}
