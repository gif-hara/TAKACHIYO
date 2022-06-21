using TAKACHIYO.BootSystems;
using TAKACHIYO.UISystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.SceneControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class FieldMapSceneController : MonoBehaviour
    {
        [SerializeField]
        private FieldMapUIPresenter fieldMapUIPresenter;
        
        private async void Start()
        {
            await BootSystem.Ready;
            await UIManager.Instance.OpenAsync(this.fieldMapUIPresenter);
        }
    }
}
