using Cysharp.Threading.Tasks;
using TAKACHIYO.UISystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class UIPresenter : MonoBehaviour, IUIPresenter
    {
        public virtual async UniTask UIInitialize()
        {
        }
        
        public virtual async void UIFinalize()
        {
        }

        public virtual async UniTask OpenAsync()
        {
            this.gameObject.SetActive(true);
        }
        
        public virtual async UniTask CloseAsync()
        {
        }
    }
}
