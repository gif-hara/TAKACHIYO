using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TAKACHIYO.MasterDataSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace TAKACHIYO.BootSystems
{
    /// <summary>
    /// 
    /// </summary>
    public static class BootSystem
    {
        public static UniTask Ready;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void Initialize()
        {
            Ready = UniTask.WhenAll(
                InitializeLocalization(),
                MasterData.SetupAsync().ToUniTask(),
                GameDesignParameter.LoadAsync()
                );
        }
        
        private static async UniTask InitializeLocalization()
        {
            await LocalizationSettings.InitializationOperation.Task;
        }
    }
}
