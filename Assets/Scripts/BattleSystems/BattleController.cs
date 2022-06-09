using TAKACHIYO.ActorControllers;
using TAKACHIYO.BootSystems;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace TAKACHIYO.BattleSystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BattleController : MonoBehaviour
    {
        [SerializeField]
        private ActorSetupData playerSetupData;

        [SerializeField]
        private ActorSetupData enemySetupData;
        
        public static IMessageBroker Broker { get; } = new MessageBroker();
        
        private async void Start()
        {
            await BootSystem.Ready;

            var player = new Actor(this.playerSetupData);
            var enemy = new Actor(this.enemySetupData);

            Observable.WhenAll(
                    player.SetupAsync(enemy),
                    enemy.SetupAsync(player)
                    )
                .Subscribe(_ =>
                {
                    Broker.Publish(BattleEvent.SetupBattle.Get(player, enemy));
                    Broker.Publish(BattleEvent.StartBattle.Get());
                });

            Broker.Receive<BattleEvent.StartBattle>()
                .TakeUntil(Broker.Receive<BattleEvent.EndBattle>())
                .Subscribe(_ =>
                {
                    this.UpdateAsObservable()
                        .TakeUntil(Broker.Receive<BattleEvent.EndBattle>())
                        .Subscribe(__ =>
                        {
                            player.CommandController.Update(Time.deltaTime);
                            enemy.CommandController.Update(Time.deltaTime);
                        });
                });
            
            // プレイヤーか敵のどちらかが死んだ場合にバトルを終了する
            Observable.Merge(
                    player.StatusController.HitPoint.Where(x => x <= 0),
                    enemy.StatusController.HitPoint.Where(x => x <= 0)
                    )
                .Subscribe(_ =>
                {
                    if (player.StatusController.IsDead && enemy.StatusController.IsDead)
                    {
                        Broker.Publish(BattleEvent.EndBattle.Get(Define.BattleJudgeType.Draw));
                    }
                    else if (enemy.StatusController.IsDead)
                    {
                        Broker.Publish(BattleEvent.EndBattle.Get(Define.BattleJudgeType.PlayerWin));
                    }
                    else
                    {
                        Broker.Publish(BattleEvent.EndBattle.Get(Define.BattleJudgeType.EnemyWin));
                    }
                });

            // TODO: 不要になったら消す
            Broker.Receive<BattleEvent.EndBattle>()
                .First()
                .Subscribe(x =>
                {
                    Debug.Log(x.BattleJudgeType);
                });
        }
    }
}
