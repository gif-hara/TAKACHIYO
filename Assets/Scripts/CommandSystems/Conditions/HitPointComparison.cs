using System;
using TAKACHIYO.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.CommandSystems.Conditions
{
    /// <summary>
    /// HPの比較で実行可能
    /// </summary>
    public sealed class HitPointComparison : CommandCondition
    {
        [SerializeField]
        private Define.TargetType targetType;
        
        [SerializeField]
        private Define.CompareType compareType;
        
        [SerializeField, Range(0.0f, 1.0f)]
        private float rate = 1.0f;

        /// <summary>
        /// 実行できる回数
        /// 0だと無限に利用可能
        /// </summary>
        [SerializeField]
        private int number;
        
        public override bool Evaluate(Command command)
        {
            if (this.number != 0 && this.number <= command.InvokedCount)
            {
                return false;
            }
            
            var actor = command.Owner.GetTarget(this.targetType);
            switch (this.compareType)
            {
                case Define.CompareType.Greater:
                    return actor.StatusController.HitPointRate >= this.rate;
                case Define.CompareType.Less:
                    return actor.StatusController.HitPointRate <= this.rate;
                default:
                    Assert.IsTrue(false, $"{this.compareType}は未対応です");
                    return false;
            }
        }
    }
}
