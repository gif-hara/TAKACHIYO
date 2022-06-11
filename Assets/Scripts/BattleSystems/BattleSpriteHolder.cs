using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.BattleSystems
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "TAKACHIYO/BattleSpriteHolder")]
    public sealed class BattleSpriteHolder : ScriptableObject
    {
        public static BattleSpriteHolder Instance { get; private set; }
        
        [SerializeField]
        private List<AbnormalStatusSprite> abnormalStatusSprites;

        public static async UniTask SetupAsync()
        {
            if (Instance != null)
            {
                Assert.IsTrue(false, $"{nameof(BattleSpriteHolder)}は既にセットアップ済みです");
            }
            else
            {
                Instance = await AssetLoader.LoadAsync<BattleSpriteHolder>("Assets/DataSources/BattleSpriteHolder.asset");
            }
        }

        public Sprite GetAbnormalStatusSprite(Define.AbnormalStatusType abnormalStatusType)
        {
            var result = this.abnormalStatusSprites.Find(x => x.abnormalStatusType == abnormalStatusType);
            Assert.IsNotNull(result, $"{abnormalStatusType}は存在しません");

            return result.sprite;
        }
        
        [Serializable]
        private class AbnormalStatusSprite
        {
            public Define.AbnormalStatusType abnormalStatusType;

            public Sprite sprite;
        }
    }
}
