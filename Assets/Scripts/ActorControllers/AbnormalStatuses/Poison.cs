using System;
using TAKACHIYO.BattleSystems;
using UniRx;
using UnityEngine;

namespace TAKACHIYO.ActorControllers.AbnormalStatuses
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Poison : AbnormalStatus
    {
        public override void Setup(Actor owner)
        {
            var parameter = GameDesignParameter.Instance;
            var seconds = parameter.PoisonDamageSeconds / parameter.PoisonDamageCount;
            Observable.Interval(TimeSpan.FromSeconds(seconds))
                .TakeUntil(BattleController.Broker.Receive<BattleEvent.EndBattle>())
                .Take(parameter.PoisonDamageCount)
                .Subscribe(_ =>
                {
                    var damage = Mathf.FloorToInt((owner.StatusController.HitPointMax.Value * parameter.PoisonDamageRate) / parameter.PoisonDamageCount);
                    owner.StatusController.TakeDamageRaw(damage);
                }, () =>
                {
                    owner.AbnormalStatusController.Remove(Define.AbnormalStatusType.Poison);
                })
                .AddTo(this.disposable);
        }
    }
}
