using System;
using TAKACHIYO.BattleSystems;
using UniRx;
using UnityEngine;

namespace TAKACHIYO.ActorControllers.AbnormalStatuses
{
    /// <summary>
    /// 躓きを制御するクラス
    /// </summary>
    public sealed class Trip : AbnormalStatus
    {
        public override void Setup(Actor owner)
        {
            foreach (var castingCommand in owner.CommandController.CastingCommands)
            {
                castingCommand.Reset();
            }
            var parameter = GameDesignParameter.Instance;
            Observable.Timer(TimeSpan.FromSeconds(parameter.tripTimeSeconds))
                .TakeUntil(BattleController.Broker.Receive<BattleEvent.EndBattle>())
                .Subscribe(_ =>
                {
                    owner.AbnormalStatusController.Remove(Define.AbnormalStatusType.Trip);
                })
                .AddTo(this.disposable);
        }
    }
}
