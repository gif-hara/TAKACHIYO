using Cysharp.Threading.Tasks;
using TAKACHIYO.ActorControllers;
using TAKACHIYO.MasterDataSystems;
using TAKACHIYO.SaveData;
using TAKACHIYO.UISystems;
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
                CreateUserData(),
                UIManager.SetupAsync()
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

            for (var i = 0; i < 5; i++)
            {
                userData.AddInstanceEquipment(new InstanceEquipment("10101002"));
                userData.AddInstanceEquipment(new InstanceEquipment("10101003"));
                userData.AddInstanceEquipment(new InstanceEquipment("10102001"));
                userData.AddInstanceEquipment(new InstanceEquipment("10102002"));
                userData.AddInstanceEquipment(new InstanceEquipment("10102003"));
                userData.AddInstanceEquipment(new InstanceEquipment("10103001"));
                userData.AddInstanceEquipment(new InstanceEquipment("10103002"));
                userData.AddInstanceEquipment(new InstanceEquipment("10103003"));
                userData.AddInstanceEquipment(new InstanceEquipment("10104001"));
                userData.AddInstanceEquipment(new InstanceEquipment("10104002"));
                userData.AddInstanceEquipment(new InstanceEquipment("10104003"));
                userData.AddInstanceEquipment(new InstanceEquipment("10105001"));
                userData.AddInstanceEquipment(new InstanceEquipment("10105002"));
                userData.AddInstanceEquipment(new InstanceEquipment("10105003"));
                userData.AddInstanceEquipment(new InstanceEquipment("10201001"));
                userData.AddInstanceEquipment(new InstanceEquipment("10301001"));
                userData.AddInstanceEquipment(new InstanceEquipment("10401001"));
                userData.AddInstanceEquipment(new InstanceEquipment("10501001"));
                userData.AddInstanceEquipment(new InstanceEquipment("10601001"));
                userData.AddInstanceEquipment(new InstanceEquipment("10202001"));
                userData.AddInstanceEquipment(new InstanceEquipment("10302001"));
                userData.AddInstanceEquipment(new InstanceEquipment("10402001"));
                userData.AddInstanceEquipment(new InstanceEquipment("10502001"));
                userData.AddInstanceEquipment(new InstanceEquipment("10602001"));
                userData.AddInstanceEquipment(new InstanceEquipment("10203001"));
                userData.AddInstanceEquipment(new InstanceEquipment("10303001"));
                userData.AddInstanceEquipment(new InstanceEquipment("10403001"));
                userData.AddInstanceEquipment(new InstanceEquipment("10503001"));
                userData.AddInstanceEquipment(new InstanceEquipment("10603001"));

            }

            UserData.Instance = userData;

            var json = JsonUtility.ToJson(userData);
            var u = JsonUtility.FromJson<UserData>(json);
        }
    }
}
