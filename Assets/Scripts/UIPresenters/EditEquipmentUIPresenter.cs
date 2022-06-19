using System.Linq;
using Cysharp.Threading.Tasks;
using TAKACHIYO.SaveData;
using TAKACHIYO.UISystems;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;

namespace TAKACHIYO.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EditEquipmentUIPresenter : UIPresenter
    {
        [SerializeField]
        private EquipmentChangeUIPresenter equipmentChangeUIPresenter;

        [SerializeField]
        private EquipmentInformationUIPresenter equipmentInformationUIPresenter;

        [SerializeField]
        private EquipmentListUIPresenter equipmentListUIPresenter;

        public override async UniTask UIInitialize()
        {
            await UniTask.WhenAll(
                base.UIInitialize(),
                this.equipmentChangeUIPresenter.UIInitialize(),
                this.equipmentInformationUIPresenter.UIInitialize(),
                this.equipmentListUIPresenter.UIInitialize()
                );

            this.equipmentChangeUIPresenter.OnRequestOpenListAsObservable()
                .Subscribe(async x =>
                {
                    var targets = UserData.Instance.InstanceEquipments
                        .Where(instanceEquipment => x.ChangeTarget.CanEquip(instanceEquipment.MasterDataEquipment.equipmentType))
                        .ToList();
                    this.equipmentListUIPresenter.Setup(targets, 0);
                    await UniTask.WhenAll(
                        this.equipmentListUIPresenter.OpenAsync(),
                        this.equipmentChangeUIPresenter.CloseAsync()
                        );
                });
        }

        public override void UIFinalize()
        {
            this.equipmentChangeUIPresenter.UIFinalize();
            this.equipmentInformationUIPresenter.UIFinalize();
            this.equipmentListUIPresenter.UIFinalize();
            base.UIFinalize();
        }

        public override async UniTask OpenAsync()
        {
            await UniTask.WhenAll(
                base.OpenAsync(),
                this.equipmentChangeUIPresenter.OpenAsync(),
                this.equipmentInformationUIPresenter.OpenAsync()
                );
        }

        public override async UniTask CloseAsync()
        {
            await UniTask.WhenAll(
                base.CloseAsync(),
                this.equipmentChangeUIPresenter.CloseAsync(),
                this.equipmentInformationUIPresenter.CloseAsync(),
                this.equipmentListUIPresenter.CloseAsync()
                );
        }
    }
}
