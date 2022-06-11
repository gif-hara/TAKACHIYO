using UnityEngine;

namespace TAKACHIYO.CommandSystems.Conditions
{
    /// <summary>
    /// 状態異常が存在する場合に実行可能
    /// </summary>
    public sealed class AbnormalStatusContains : CommandCondition
    {
        [SerializeField]
        private Define.TargetType targetType;
        
        [SerializeField]
        private Define.AbnormalStatusType abnormalStatusType;

        /// <summary>
        /// <c>true</c>の場合は状態異常が存在していれば実行可能
        /// <c>false</c>の場合は状態異常が存在していなければ実行可能
        /// </summary>
        [SerializeField]
        private bool isContains;

        /// <summary>
        /// 実行できる回数
        /// 0だと無限に利用可能
        /// </summary>
        [SerializeField]
        private int number;
        
        public override bool Evaluate(Command command)
        {
            if (this.number != 0 && this.number <= command.InvokeCount)
            {
                return false;
            }
            
            var actor = command.Owner.GetTarget(this.targetType);
            return actor.AbnormalStatusController.Contains(this.abnormalStatusType) == this.isContains;
        }
    }
}
