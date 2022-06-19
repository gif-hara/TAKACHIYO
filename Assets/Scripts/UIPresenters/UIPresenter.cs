using Cysharp.Threading.Tasks;
using TAKACHIYO.UISystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class UIPresenter : MonoBehaviour, IUIPresenter
    {
        protected readonly Subject<Unit> onFinalizeSubject = new();
        
        public virtual async UniTask UIInitialize()
        {
            this.gameObject.SetActive(false);
        }
        
        public virtual async void UIFinalize()
        {
            this.onFinalizeSubject.OnNext(Unit.Default);
            this.onFinalizeSubject.Dispose();
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
