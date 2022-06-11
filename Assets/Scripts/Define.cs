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

        public enum AbnormalStatusType
        {
            /// <summary>
            /// 毒
            /// 一定時間ダメージを受ける
            /// </summary>
            Poison,
            
            /// <summary>
            /// 麻痺
            /// 一定時間詠唱時間が倍になる
            /// </summary>
            Paralysis,
            
            /// <summary>
            /// 睡眠
            /// 一定時間詠唱ができない
            /// </summary>
            Sleep,
            
            /// <summary>
            /// 脱力
            /// 一定時間与えるダメージが減る
            /// </summary>
            Exhaustion,
            
            /// <summary>
            /// 脆弱
            /// 一定時間受けるダメージが増える
            /// </summary>
            Brittle,
            
            /// <summary>
            /// 躓き
            /// 詠唱時間がリセットされる
            /// </summary>
            Trip,
            
            /// <summary>
            /// 癒し
            /// 一定時間回復する
            /// </summary>
            Healing,
            
            /// <summary>
            /// 俊足
            /// 一定時間詠唱時間が早くなる
            /// </summary>
            FleetSpeed,
            
            /// <summary>
            /// 怪力
            /// 一定時間与えるダメージが増える
            /// </summary>
            Strong,
            
            /// <summary>
            /// 頑強
            /// 一定時間受けるダメージが減る
            /// </summary>
            Stubborn,
            
            /// <summary>
            /// 体力増強
            /// 一定時間HPが増える
            /// </summary>
            PhysicalStrength,
        }
    }
}
