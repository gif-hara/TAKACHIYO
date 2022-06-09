using TAKACHIYO.ActorControllers;
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

        public abstract void Invoke(Actor owner);
    }
}
