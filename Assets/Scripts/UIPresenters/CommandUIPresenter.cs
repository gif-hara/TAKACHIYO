using System;
using System.Collections;
using System.Collections.Generic;
using TAKACHIYO.CommandSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace TAKACHIYO
{
    public class CommandUIPresenter : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI commandName;

        [SerializeField]
        private Slider castTimeSlider;

        [SerializeField]
        private AnimationController animationController;

        [SerializeField]
        private AnimationClip inAnimation;

        [SerializeField]
        private AnimationClip outAnimation;

        public void Setup(Command command)
        {
            this.commandName.text = command.CommandName;
            command.CurrentCastTime
                .TakeUntilDestroy(this)
                .Subscribe(_ =>
                {
                    this.castTimeSlider.value = command.CastTimeRate;
                });
        }

        public void PlayInAnimation()
        {
            this.animationController.Play(this.inAnimation);
        }

        public IObservable<Unit> PlayOutAnimationAsync()
        {
            return this.animationController.PlayAsync(this.outAnimation);
        }
    }
}
