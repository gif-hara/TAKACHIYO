using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EquipmentInformationCommandConditionUIView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI message;

        public string Message
        {
            set => this.message.text = value;
        }
    }
}
