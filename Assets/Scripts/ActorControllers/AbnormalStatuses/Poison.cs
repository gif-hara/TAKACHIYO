using System;
using TAKACHIYO.BattleSystems;
using UniRx;
using UnityEngine;

namespace TAKACHIYO.ActorControllers.AbnormalStatuses
{
    /// <summary>
    /// 毒を制御するクラス
    /// </summary>
    public sealed class Poison : AbnormalStatus
    {
        public override void Setup(Actor owner)
        {
            var parameter = GameDesignParameter.Instance;
            var seconds = parameter.poisonDamageSeconds / parameter.poisonDamageCount;
            Observable.Interval(TimeSpan.FromSeconds(seconds))
                .TakeUntil(BattleController.Broker.Receive<BattleEvent.EndBattle>())
                .Take(parameter.poisonDamageCount)
                .Subscribe(_ =>
                {
                    var damage = Mathf.FloorToInt((owner.StatusController.HitPointMax.Value * parameter.poisonDamageRate) / parameter.poisonDamageCount);
                    owner.StatusController.TakeDamageRaw(damage);
                }, () =>
                {
                    owner.AbnormalStatusController.Remove(Define.AbnormalStatusType.Poison);
                })
                .AddTo(this.disposable);
        }
    }
}
