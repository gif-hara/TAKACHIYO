using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DamageElementUIView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI damageLabel;

        [SerializeField]
        private AnimationController animationController;

        [SerializeField]
        private AnimationClip animationClip;

        public string Damage
        {
            set => this.damageLabel.text = value;
        }

        public IObservable<Unit> PlayAnimationAsync()
        {
            return this.animationController.PlayAsync(this.animationClip);
        }
    }
}
