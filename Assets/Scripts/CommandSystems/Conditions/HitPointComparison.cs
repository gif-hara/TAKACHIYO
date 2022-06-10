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
        
        public override bool Evaluate(Command command)
        {
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
