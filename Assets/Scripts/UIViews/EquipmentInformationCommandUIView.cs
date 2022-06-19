using HK.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EquipmentInformationCommandUIView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI commandName;

        [SerializeField]
        private TextMeshProUGUI commandDescription;

        [SerializeField]
        private TextMeshProUGUI castTime;

        [SerializeField]
        private TextMeshProUGUI power;

        [SerializeField]
        private GameObject powerRoot;

        [SerializeField]
        private Transform conditionRoot;

        [SerializeField]
        private EquipmentInformationCommandConditionUIView conditionUIView;

        private ObjectPool<EquipmentInformationCommandConditionUIView> conditionUIViewPool;

        public string CommandName
        {
            set => this.commandName.text = value;
        }

        public string CommandDescription
        {
            set => this.commandDescription.text = value;
        }

        public string CastTime
        {
            set => this.castTime.text = value;
        }
        
        public string Power
        {
            set => this.power.text = value;
        }

        public void SetActivePowerRoot(bool isActive)
        {
            this.powerRoot.SetActive(isActive);
        }

        public void CreateConditionUIView(string message)
        {
            this.conditionUIViewPool ??= new ObjectPool<EquipmentInformationCommandConditionUIView>(this.conditionUIView);

            var instance = this.conditionUIViewPool.Rent();
            instance.transform.SetParent(this.conditionRoot, false);
            instance.Message = message;
        }

        public void ReturnAllConditionUIView()
        {
            this.conditionUIViewPool?.ReturnAll();
        }
    }
}
