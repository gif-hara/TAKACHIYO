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
        public int physicsStrength;

        public int magicStrength;

        public int recoveryPower;

        /// <summary>
        /// 物理攻撃力
        /// </summary>
        public int PhysicsStrength => this.physicsStrength;

        public int MagicStrength => this.magicStrength;

        /// <summary>
        /// 回復力
        /// </summary>
        public int RecoveryPower => this.recoveryPower;
    }
}
