using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace TAKACHIYO
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EditEquipmentButtonUIView : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private Image thumbnail;

        public IObservable<Unit> OnClickedButtonAsObservable() => this.button.OnClickAsObservable();

        public Sprite Thumbnail
        {
            set => this.thumbnail.sprite = value;
        }
    }
}
