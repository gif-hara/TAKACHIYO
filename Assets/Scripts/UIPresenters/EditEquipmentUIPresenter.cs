using Cysharp.Threading.Tasks;
using HK.Framework;
using TAKACHIYO.BootSystems;
using TAKACHIYO.SaveData;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EditEquipmentUIPresenter : AnimatableUIPresenter
    {
        [SerializeField]
        private Transform currentEquipmentParent;

        [SerializeField]
        private EditEquipmentButtonUIView editEquipmentButtonPrefab;

        private ObjectPool<EditEquipmentButtonUIView> buttonPool;

        public override UniTask UIInitialize()
        {
            this.buttonPool = new ObjectPool<EditEquipmentButtonUIView>(this.editEquipmentButtonPrefab);
            return base.UIInitialize();
        }
        
        public override async UniTask OpenAsync()
        {
            await UniTask.WhenAll(
                this.CreateButton(Define.EquipmentPartType.MainWeapon),
                this.CreateButton(Define.EquipmentPartType.ArmorHead),
                this.CreateButton(Define.EquipmentPartType.ArmorChest),
                this.CreateButton(Define.EquipmentPartType.SubWeapon1),
                this.CreateButton(Define.EquipmentPartType.ArmorArms),
                this.CreateButton(Define.EquipmentPartType.ArmorTorso),
                this.CreateButton(Define.EquipmentPartType.SubWeapon2),
                this.CreateButton(Define.EquipmentPartType.ArmorLegs),
                this.CreateButton(Define.EquipmentPartType.Accessory)
                );
            await base.OpenAsync();
        }

        public override void UIFinalize()
        {
            this.buttonPool.Dispose();
            base.UIFinalize();
        }

        private async UniTask CreateButton(Define.EquipmentPartType partType)
        {
            var button = this.buttonPool.Rent();
            button.transform.SetParent(this.currentEquipmentParent, false);
            var instanceEquipment = UserData.Instance.ActorEquipment.GetOrNull(partType);
            if (instanceEquipment != null)
            {
                button.Thumbnail = await instanceEquipment.MasterDataEquipment.GetThumbnail();
                button.SetActiveThumbnail(true);
            }
            else
            {
                button.Thumbnail = null;
                button.SetActiveThumbnail(false);
            }
        }
    }
}