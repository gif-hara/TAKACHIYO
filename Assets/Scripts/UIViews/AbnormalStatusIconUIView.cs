using TAKACHIYO.BattleSystems;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace TAKACHIYO.UIViews
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AbnormalStatusIconUIView : MonoBehaviour
    {
        [SerializeField]
        private Image icon;

        public void Setup(Sprite sprite)
        {
            this.icon.sprite = sprite;
        }
    }
}
