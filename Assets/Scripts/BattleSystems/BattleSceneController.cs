using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TAKACHIYO.ActorControllers;
using TAKACHIYO.BootSystems;
using TAKACHIYO.UISystems;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace TAKACHIYO.BattleSystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BattleSceneController : MonoBehaviour
    {
        [SerializeField]
        private BattleUIPresenter battleUIPresenter;
        
        [SerializeField]
        private EquipmentActorSetupData playerSetupData;

        [SerializeField]
        private List<DebugActorSetupData> enemySetupDataList;

        [SerializeField]
        private int initialEnemyIndex;
        
        private int currentEnemyIndex = 0;

        private Actor player;
        
        public static IMessageBroker Broker { get; } = new MessageBroker();
        
        private async void Start()
        {
            await BootSystem.Ready;
            await BattleSpriteHolder.SetupAsync();
            await UIManager.Instance.OpenAsync(this.battleUIPresenter);

            this.player = new Actor(this.playerSetupData);
            this.currentEnemyIndex = this.initialEnemyIndex;

            await this.SetupAsync(this.enemySetupDataList[this.currentEnemyIndex]);
        }

        private async UniTask SetupAsync(IActorSetupData enemySetupData)
        {
            var enemy = new Actor(enemySetupData);
            Broker.Receive<BattleEvent.StartBattle>()
                .TakeUntil(Broker.Receive<BattleEvent.EndBattle>())
                .Subscribe(_ =>
                {
                    this.UpdateAsObservable()
                        .TakeUntil(Broker.Receive<BattleEvent.EndBattle>())
                        .Subscribe(__ =>
                        {
                            this.player.CommandController.Update(Time.deltaTime);
                            enemy.CommandController.Update(Time.deltaTime);
                        });
                });
            
            // プレイヤーか敵のどちらかが死んだ場合にバトルを終了する
            Observable.Merge(
                    this.player.StatusController.HitPoint.Where(x => x <= 0),
                    enemy.StatusController.HitPoint.Where(x => x <= 0)
                    )
                .TakeUntil(Broker.Receive<BattleEvent.EndBattle>())
                .DelayFrame(1)
                .Subscribe(_ =>
                {
                    if (this.player.StatusController.IsDead && enemy.StatusController.IsDead)
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
                .SelectMany(x => Observable.Timer(TimeSpan.FromSeconds(3.0f)).Select(_ => x))
                .Subscribe(async x =>
                {
                    if (x.BattleJudgeType == Define.BattleJudgeType.PlayerWin)
                    {
                        this.currentEnemyIndex++;
                        if (this.currentEnemyIndex < this.enemySetupDataList.Count)
                        {
                            await this.SetupAsync(this.enemySetupDataList[this.currentEnemyIndex]);
                        }
                        else
                        {
                            Debug.Log("TODO: 全ての敵を倒した後のフロー");
                        }
                    }
                    else
                    {
                        Debug.Log("TODO: 負けた時のフロー");
                    }
                });
            
            await UniTask.WhenAll(
                this.player.SetupAsync(enemy),
                enemy.SetupAsync(this.player)
                );
            
            Broker.Publish(BattleEvent.SetupBattle.Get(player, enemy));
            Broker.Publish(BattleEvent.StartBattle.Get());
        }
    }
}
