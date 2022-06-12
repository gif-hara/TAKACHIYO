using Cysharp.Threading.Tasks;
using TAKACHIYO.MasterDataSystems;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace TAKACHIYO.BootSystems
{
    /// <summary>
    /// ブートシステム
    /// </summary>
    public static class BootSystem
    {
        /// <summary>
        /// アプリが利用可能な状態になったか
        /// </summary>
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
