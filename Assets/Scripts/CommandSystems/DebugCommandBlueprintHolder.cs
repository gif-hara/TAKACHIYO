using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.CommandSystems
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class DebugCommandBlueprintHolder : ICommandBlueprintHolder
    {
        public int strength;

        /// <summary>
        /// 攻撃力
        /// </summary>
        public int Strength => this.strength;
    }
}
