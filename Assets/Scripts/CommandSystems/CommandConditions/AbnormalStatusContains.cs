using System;
using TAKACHIYO.ActorControllers;
using TAKACHIYO.BattleSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Localization;

namespace TAKACHIYO.CommandSystems.CommandConditions
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

        public override IObservable<Unit> TryCastAsObservable(Actor owner)
        {
            return Observable.Defer(() =>
            {
                var target = owner.GetTarget(this.targetType);

                return Observable.Merge(
                    BattleSceneController.Broker.Receive<BattleEvent.StartBattle>().AsUnitObservable(),
                    owner.Broker.Receive<ActorEvent.InvokedCommand>().AsUnitObservable(),
                    target.Broker.Receive<ActorEvent.AddedAbnormalStatus>().AsUnitObservable(),
                    target.Broker.Receive<ActorEvent.RemovedAbnormalStatus>().AsUnitObservable()
                    );
            });
        }
        
        public override bool Evaluate(Command command)
        {
            if (this.number != 0 && this.number <= command.InvokedCount)
            {
                return false;
            }
            
            var actor = command.Owner.GetTarget(this.targetType);
            return actor.AbnormalStatusController.Contains(this.abnormalStatusType) == this.isContains;
        }
        
        public override string LocalizedDescription
        {
            get
            {
                if (this.number <= 0)
                {
                    return string.Format(
                        new LocalizedString("Common", "Condition.AbnormalStatusContains.Always").GetLocalizedString(),
                        this.targetType.LocalizedString(),
                        this.abnormalStatusType.LocalizedName(),
                        this.isContains.LocalizedStringDoNotDo()
                        );
                }
                else
                {
                    return string.Format(
                        new LocalizedString("Common", "Condition.AbnormalStatusContains.Number").GetLocalizedString(),
                        this.targetType.LocalizedString(),
                        this.abnormalStatusType.LocalizedName(),
                        this.isContains.LocalizedStringDoNotDo(),
                        this.number
                        );
                }
            }
        }
    }
}
