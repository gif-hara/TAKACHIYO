using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO
{
    /// <summary>
    /// 
    /// </summary>
    public static class Define
    {
        public enum ActorType
        {
            Player,
            Enemy,
        }

        public enum TargetType
        {
            My,
            Opponent,
            All,
        }

        public enum BattleJudgeType
        {
            PlayerWin,
            EnemyWin,
            Draw,
        }

        /// <summary>
        /// 比較タイプ
        /// </summary>
        public enum CompareType
        {
            /// <summary>
            /// 以上
            /// </summary>
            Greater,
            
            /// <summary>
            /// 以下
            /// </summary>
            Less
        }
    }
}
