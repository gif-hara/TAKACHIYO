using TAKACHIYO.BootSystems;
using TAKACHIYO.UISystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.SceneControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EditEquipmentSceneController : MonoBehaviour
    {
        [SerializeField]
        private EditEquipmentUIPresenter editEquipmentUIPresenter;
        
        private async void Start()
        {
            await BootSystem.Ready;
            await UIManager.Instance.OpenAsync(this.editEquipmentUIPresenter);
        }
    }
}
