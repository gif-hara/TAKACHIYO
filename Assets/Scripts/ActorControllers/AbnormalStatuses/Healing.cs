using System;
using TAKACHIYO.BattleSystems;
using UniRx;
using UnityEngine;

namespace TAKACHIYO.ActorControllers.AbnormalStatuses
{
    /// <summary>
    /// 癒しを制御するクラス
    /// </summary>
    public sealed class Healing : AbnormalStatus
    {
        public override void Setup(Actor owner)
        {
            var parameter = GameDesignParameter.Instance;
            var seconds = parameter.healingDamageSeconds / parameter.healingDamageCount;
            Observable.Interval(TimeSpan.FromSeconds(seconds))
                .TakeUntil(BattleController.Broker.Receive<BattleEvent.EndBattle>())
                .Take(parameter.healingDamageCount)
                .Subscribe(_ =>
                {
                    var damage = Mathf.FloorToInt((owner.StatusController.HitPointMax.Value * parameter.healingDamageRate) / parameter.healingDamageCount);
                    owner.StatusController.TakeDamageRaw(-damage, true);
                }, () =>
                {
                    owner.AbnormalStatusController.Remove(Define.AbnormalStatusType.Healing);
                })
                .AddTo(this.disposable);
        }
    }
}
