using System;
using TAKACHIYO.ActorControllers;
using TAKACHIYO.BootSystems;
using TAKACHIYO.MasterDataSystems;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

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
                    this.Player.SetupAsync(),
                    this.Enemy.SetupAsync()
                    )
                .Subscribe(_ =>
                {
                    Broker.Publish(BattleEvent.OnSetupBattle.Get(this.Player, this.Enemy));
                    Broker.Publish(BattleEvent.BattleStart.Get());
                });

            Broker.Receive<BattleEvent.BattleStart>()
                .TakeUntil(Broker.Receive<BattleEvent.BattleEnd>())
                .Subscribe(_ =>
                {
                    this.UpdateAsObservable()
                        .TakeUntil(Broker.Receive<BattleEvent.BattleEnd>())
                        .Subscribe(__ =>
                        {
                            this.Player.CommandController.Update(Time.deltaTime);
                            this.Enemy.CommandController.Update(Time.deltaTime);
                        });
                });
        }
    }
}
