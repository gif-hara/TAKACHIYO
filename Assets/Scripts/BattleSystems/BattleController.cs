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
        
        public Actor Player { get; private set; }
        
        public Actor Enemy { get; private set; }
        
        public static IMessageBroker Broker { get; } = new MessageBroker();
        
        private async void Start()
        {
            await BootSystem.Ready;

            this.Player = new Actor(this.playerSetupData);
            this.Enemy = new Actor(this.enemySetupData);

            Observable.WhenAll(
                    this.Player.SetupAsync(this.Enemy),
                    this.Enemy.SetupAsync(this.Player)
                    )
                .Subscribe(_ =>
                {
                    Broker.Publish(BattleEvent.SetupBattle.Get(this.Player, this.Enemy));
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
                            this.Player.CommandController.Update(Time.deltaTime);
                            this.Enemy.CommandController.Update(Time.deltaTime);
                        });
                });
            
            // プレイヤーか敵のどちらかが死んだ場合にバトルを終了する
            Observable.Merge(
                    this.Player.StatusController.HitPoint.Where(x => x <= 0),
                    this.Enemy.StatusController.HitPoint.Where(x => x <= 0)
                    )
                .Subscribe(_ =>
                {
                    if (this.Player.StatusController.IsDead && this.Enemy.StatusController.IsDead)
                    {
                        Broker.Publish(BattleEvent.EndBattle.Get(Define.BattleJudgeType.Draw));
                    }
                    else if (this.Enemy.StatusController.IsDead)
                    {
                        Broker.Publish(BattleEvent.EndBattle.Get(Define.BattleJudgeType.PlayerWin));
                    }
                    else
                    {
                        Broker.Publish(BattleEvent.EndBattle.Get(Define.BattleJudgeType.EnemyWin));
                    }
                });
        }
    }
}
