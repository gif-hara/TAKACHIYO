using Cysharp.Threading.Tasks;
using TAKACHIYO.ActorControllers;
using TAKACHIYO.MasterDataSystems;
using TAKACHIYO.SaveData;
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
                GameDesignParameter.LoadAsync(),
                CreateUserData()
                );
            
        }
        
        private static async UniTask InitializeLocalization()
        {
            await LocalizationSettings.InitializationOperation.Task;
        }

        /// <summary>
        /// ユーザーデータを作成する
        /// TODO: 不要になったら削除する
        /// </summary>
        private static async UniTask CreateUserData()
        {
            var actorEquipment = new ActorEquipment();
            actorEquipment.InstanceEquipments.Add(new ActorEquipment.InstanceEquipmentData
            {
                equipmentPartType = Define.EquipmentPartType.MainWeapon,
                instanceEquipment = new InstanceEquipment("10101001")
            });
            actorEquipment.InstanceEquipments.Add(new ActorEquipment.InstanceEquipmentData
            {
                equipmentPartType = Define.EquipmentPartType.ArmorHead,
                instanceEquipment = new InstanceEquipment("10201001")
            });
            actorEquipment.InstanceEquipments.Add(new ActorEquipment.InstanceEquipmentData
            {
                equipmentPartType = Define.EquipmentPartType.ArmorChest,
                instanceEquipment = new InstanceEquipment("10301001")
            });
            actorEquipment.InstanceEquipments.Add(new ActorEquipment.InstanceEquipmentData
            {
                equipmentPartType = Define.EquipmentPartType.ArmorArms,
                instanceEquipment = new InstanceEquipment("10401001")
            });
            actorEquipment.InstanceEquipments.Add(new ActorEquipment.InstanceEquipmentData
            {
                equipmentPartType = Define.EquipmentPartType.ArmorTorso,
                instanceEquipment = new InstanceEquipment("10501001")
            });
            actorEquipment.InstanceEquipments.Add(new ActorEquipment.InstanceEquipmentData
            {
                equipmentPartType = Define.EquipmentPartType.ArmorLegs,
                instanceEquipment = new InstanceEquipment("10601001")
            });

            var userData = new UserData("1", actorEquipment);
            userData.SetName("Hoge");

            var json = JsonUtility.ToJson(userData);

            var u = JsonUtility.FromJson<UserData>(json);
        }
    }
}
