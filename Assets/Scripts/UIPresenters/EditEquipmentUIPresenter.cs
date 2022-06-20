using System;
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
                    IDisposable selectInstanceEquipment = null;
                    selectInstanceEquipment = this.equipmentListUIPresenter
                        .SelectInstanceEquipmentAsObservable()
                        .Subscribe(instanceEquipment =>
                        {
                            UserData.Instance.ActorEquipment.AddOrUpdate(x.ChangeTarget, instanceEquipment);
                            this.OpenEquipmentChange();
                            selectInstanceEquipment?.Dispose();
                        });
                    await UniTask.WhenAll(
                        this.equipmentListUIPresenter.OpenAsync(),
                        this.equipmentChangeUIPresenter.CloseAsync()
                        );
                });
            
            this.equipmentInformationUIPresenter.RegisterSetup(
                this.equipmentChangeUIPresenter.OnMouseEnterInstanceEquipmentAsObservable(),
                this.onFinalizeSubject
                );
            
            this.equipmentInformationUIPresenter.RegisterSetup(
                this.equipmentListUIPresenter.OnMouseEnterInstanceEquipmentAsObservable(),
                this.onFinalizeSubject
                );
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

        private void OpenEquipmentChange()
        {
            UniTask.WhenAll(
                this.equipmentChangeUIPresenter.OpenAsync(),
                this.equipmentListUIPresenter.CloseAsync()
                );
        }
    }
}
