using System;
using TAKACHIYO.ActorControllers;
using TAKACHIYO.BootSystems;
using TAKACHIYO.MasterDataSystems;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GameController : MonoBehaviour
    {
        [SerializeField]
        private string playerId;

        [SerializeField]
        private string enemyId;

        public static IMessageBroker Broker { get; } = new MessageBroker();
        
        private async void Start()
        {
            await BootSystem.Ready;

            var player = new Actor(MasterDataActorStatus.Get(this.playerId));
            var enemy = new Actor(MasterDataActorStatus.Get(this.enemyId));
            
            Broker.Publish(GameEvent.OnSetupBattle.Get(player, enemy));
        }
    }
}
