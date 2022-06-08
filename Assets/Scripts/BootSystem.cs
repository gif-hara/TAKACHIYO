using System.Threading.Tasks;
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
        public static Task Ready;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void Initialize()
        {
            Ready = Task.WhenAll(
                InitializeLocalization(),
                MasterData.SetupAsync().ToTask()
                );
        }
        
        private static async Task InitializeLocalization()
        {
            await LocalizationSettings.InitializationOperation.Task;
        }
    }
}
