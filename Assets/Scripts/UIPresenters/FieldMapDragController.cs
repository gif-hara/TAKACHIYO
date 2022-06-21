using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace TAKACHIYO
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class FieldMapDragController : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        private RectTransform rectTransform;

        private void Awake()
        {
            this.rectTransform = (RectTransform)this.transform;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            var localScale = this.rectTransform.localScale;
            var x = -eventData.delta.x / Screen.width / localScale.x;
            var y = -eventData.delta.y / Screen.height / localScale.y;
            this.rectTransform.pivot += new Vector2(x, y);
        }
    }
}
