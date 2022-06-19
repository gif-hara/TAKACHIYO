using System;
using TAKACHIYO.BattleSystems;
using UniRx;

namespace TAKACHIYO.ActorControllers.AbnormalStatuses
{
    /// <summary>
    /// 一定時間が過ぎると削除される状態異常
    /// </summary>
    public sealed class TimerAbnormalStatus : AbnormalStatus
    {
        private readonly Define.AbnormalStatusType abnormalStatusType;

        private readonly float delaySeconds;
        
        public TimerAbnormalStatus(Define.AbnormalStatusType abnormalStatusType, float delaySeconds)
        {
            this.abnormalStatusType = abnormalStatusType;
            this.delaySeconds = delaySeconds;
        }

        public override void Setup(Actor owner)
        {
            base.Setup(owner);

            Observable.Timer(TimeSpan.FromSeconds(this.delaySeconds))
                .TakeUntil(BattleSceneController.Broker.Receive<BattleEvent.EndBattle>())
                .Subscribe(_ =>
                {
                    owner.AbnormalStatusController.Remove(this.abnormalStatusType);
                })
                .AddTo(this.disposable);
        }
    }
}