using Cysharp.Threading.Tasks;
using TAKACHIYO.UISystems;
using UnityEngine;
using UnityEngine.Assertions;

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

        public override async UniTask UIInitialize()
        {
            await UniTask.WhenAll(
                this.equipmentChangeUIPresenter.UIInitialize(),
                this.equipmentInformationUIPresenter.UIInitialize()
                );
            
            await base.UIInitialize();
        }

        public override void UIFinalize()
        {
            this.equipmentChangeUIPresenter.UIFinalize();
            this.equipmentInformationUIPresenter.UIFinalize();
            base.UIFinalize();
        }

        public override async UniTask OpenAsync()
        {
            await UniTask.WhenAll(
                this.equipmentChangeUIPresenter.OpenAsync(),
                this.equipmentInformationUIPresenter.OpenAsync(),
                base.OpenAsync()
                );
        }

        public override async UniTask CloseAsync()
        {
            await UniTask.WhenAll(
                this.equipmentChangeUIPresenter.CloseAsync(),
                this.equipmentInformationUIPresenter.CloseAsync(),
                base.CloseAsync()
                );
        }
    }
}
