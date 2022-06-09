using TAKACHIYO.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.CommandSystems.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommandAction
    {
        void Invoke(Actor owner);
    }
}
