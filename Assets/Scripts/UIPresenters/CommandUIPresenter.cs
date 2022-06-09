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
    }
}
