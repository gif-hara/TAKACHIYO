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
            await UniTask.WhenAll(
                base.OpenAsync(),
                this.animationController.PlayAsync(this.inAnimationClip).ToUniTask()
                );
        }

        public override async UniTask CloseAsync()
        {
            await UniTask.WhenAll(
                base.CloseAsync(),
                this.animationController.PlayAsync(this.outAnimationClip).ToUniTask()
                );
        }
    }
}
