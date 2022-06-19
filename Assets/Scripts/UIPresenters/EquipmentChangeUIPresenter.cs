using Cysharp.Threading.Tasks;
using HK.Framework;
using TAKACHIYO.BootSystems;
using TAKACHIYO.SaveData;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;

namespace TAKACHIYO.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EquipmentChangeUIPresenter : AnimatableUIPresenter
    {
        [SerializeField]
        private Transform currentEquipmentParent;

        [SerializeField]
        private EquipmentButtonUIView equipmentButtonPrefab;

        [SerializeField]
        private EquipmentInformationUIPresenter equipmentInformationUIPresenter;

        private ObjectPool<EquipmentButtonUIView> buttonPool;

        public override UniTask UIInitialize()
        {
            this.buttonPool = new ObjectPool<EquipmentButtonUIView>(this.equipmentButtonPrefab);
            return base.UIInitialize();
        }
        
        public override async UniTask OpenAsync()
        {
            this.buttonPool.ReturnAll();
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
            button.Thumbnail = null;
            button.SetActiveThumbnail(false);

            var instanceEquipment = UserData.Instance.ActorEquipment.GetOrNull(partType);
            if (instanceEquipment != null)
            {
                button.Thumbnail = await instanceEquipment.MasterDataEquipment.GetThumbnail();
                button.SetActiveThumbnail(true);
            }

            button.OnMouseEnterAsObservable()
                .TakeUntilDisable(button)
                .Subscribe(async _ =>
                {
                    await this.equipmentInformationUIPresenter.SetupAsync(instanceEquipment);
                });
        }
    }
}
