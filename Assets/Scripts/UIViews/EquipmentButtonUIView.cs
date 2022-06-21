using System;
using Cysharp.Threading.Tasks;
using HK.Framework;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TAKACHIYO
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EquipmentButtonUIView : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private Image thumbnail;

        [SerializeField]
        private TextMeshProUGUI message;

        private readonly Subject<Unit> onMouseEnterSubject = new ();

        public IObservable<Unit> OnClickedButtonAsObservable() => this.button.OnClickAsObservable();

        public IObservable<Unit> OnMouseEnterAsObservable() => this.onMouseEnterSubject;

        public Sprite Thumbnail
        {
            set => this.thumbnail.sprite = value;
        }

        public string Message
        {
            set => this.message.text = value;
        }

        public void SetActiveThumbnail(bool isActive)
        {
            this.thumbnail.enabled = isActive;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            this.onMouseEnterSubject.OnNext(Unit.Default);
        }
    }
}
