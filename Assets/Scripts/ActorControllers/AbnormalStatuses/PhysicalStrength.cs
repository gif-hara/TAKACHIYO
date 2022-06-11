using System;
using TAKACHIYO.BattleSystems;
using UniRx;
using UnityEngine;

namespace TAKACHIYO.ActorControllers.AbnormalStatuses
{
    /// <summary>
    /// 体力増強を制御するクラス
    /// </summary>
    public sealed class PhysicalStrength : AbnormalStatus
    {
        /// <summary>
        /// 上昇したHPの値
        /// </summary>
        private int hitPointUpValue;
        
        public override void Setup(Actor owner)
        {
            this.hitPointUpValue = Mathf.FloorToInt(owner.StatusController.BaseStatus.hitPoint * GameDesignParameter.Instance.physicalStrengthHitPointUpRate);
            owner.StatusController.AddHitPointMax(this.hitPointUpValue);
            Observable.Timer(TimeSpan.FromSeconds(GameDesignParameter.Instance.physicalStrengthTimeSeconds))
                .TakeUntil(BattleController.Broker.Receive<BattleEvent.EndBattle>())
                .Subscribe(_ =>
                {
                    owner.StatusController.AddHitPointMax(-this.hitPointUpValue);
                    owner.AbnormalStatusController.Remove(Define.AbnormalStatusType.PhysicalStrength);
                })
                .AddTo(this.disposable);
        }
    }
}
