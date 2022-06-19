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
        private ChangeEquipmentUIPresenter changeEquipmentUIPresenter;

        [SerializeField]
        private EquipmentInformationUIPresenter equipmentInformationUIPresenter;

        public override async UniTask UIInitialize()
        {
            await UniTask.WhenAll(
                this.changeEquipmentUIPresenter.UIInitialize(),
                this.equipmentInformationUIPresenter.UIInitialize()
                );
            
            await base.UIInitialize();
        }

        public override void UIFinalize()
        {
            this.changeEquipmentUIPresenter.UIFinalize();
            this.equipmentInformationUIPresenter.UIFinalize();
            base.UIFinalize();
        }

        public override async UniTask OpenAsync()
        {
            await UniTask.WhenAll(
                this.changeEquipmentUIPresenter.OpenAsync(),
                this.equipmentInformationUIPresenter.OpenAsync(),
                base.OpenAsync()
                );
        }

        public override async UniTask CloseAsync()
        {
            await UniTask.WhenAll(
                this.changeEquipmentUIPresenter.CloseAsync(),
                this.equipmentInformationUIPresenter.CloseAsync(),
                base.CloseAsync()
                );
        }
    }
}
