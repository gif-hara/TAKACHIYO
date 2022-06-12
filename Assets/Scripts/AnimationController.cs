using System;
using UniRx;
using UniRx.Triggers;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AnimationController : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private AnimatorOverrideController overrideController;
        
        private const string OverrideClipName = "Clip";

        private void Awake()
        {
            this.overrideController = new AnimatorOverrideController();
            this.overrideController.runtimeAnimatorController = this.animator.runtimeAnimatorController;
            this.animator.runtimeAnimatorController = this.overrideController;
        }

        public void Play(AnimationClip clip)
        {
            this.ChangeClip(clip);
        }

        public IObservable<Unit> PlayAsync(AnimationClip clip)
        {
            this.ChangeClip(clip);

            return this.UpdateAsObservable()
                .Where(_ => this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                .Take(1)
                .AsUnitObservable();
        }

        private void ChangeClip(AnimationClip clip)
        {
            this.overrideController[OverrideClipName] = clip;
            for (var i = 0; i < this.animator.layerCount; i++)
            {
                this.animator.Play(this.animator.GetCurrentAnimatorStateInfo(i).fullPathHash, i, 0.0f);
            }
            
            this.animator.Update(0.0f);
        }
    }
}
