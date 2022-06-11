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

        public int recoveryPower;

        /// <summary>
        /// 攻撃力
        /// </summary>
        public int Strength => this.strength;

        /// <summary>
        /// 回復力
        /// </summary>
        public int RecoveryPower => this.recoveryPower;
    }
}
