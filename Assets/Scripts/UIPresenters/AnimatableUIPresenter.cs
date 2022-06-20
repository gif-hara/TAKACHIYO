using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TAKACHIYO.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AnimatableUIPresenter : UIPresenter
    {
        [SerializeField]
        private AnimationController animationController;

        [SerializeField]
        private AnimationClip inAnimationClip;

        [SerializeField]
        private AnimationClip outAnimationClip;
        
        public override async UniTask OpenAsync()
        {
            this.isOpen = true;
            this.gameObject.SetActive(true);
            await this.animationController.PlayAsync(this.inAnimationClip).ToUniTask();
        }

        public override async UniTask CloseAsync()
        {
            this.isOpen = false;
            await this.animationController.PlayAsync(this.outAnimationClip).ToUniTask();

            if (!this.isOpen)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
