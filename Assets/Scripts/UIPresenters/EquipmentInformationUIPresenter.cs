using TAKACHIYO.ActorControllers;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EquipmentInformationUIPresenter : UIPresenter
    {
        [SerializeField]
        private GameObject root;
        
        [SerializeField]
        private TextMeshProUGUI equipmentName;

        [SerializeField]
        private TextMeshProUGUI hitPoint;

        [SerializeField]
        private TextMeshProUGUI physicsStrength;

        [SerializeField]
        private TextMeshProUGUI physicsDefense;

        [SerializeField]
        private TextMeshProUGUI magicStrength;

        [SerializeField]
        private TextMeshProUGUI magicDefense;

        [SerializeField]
        private TextMeshProUGUI speed;

        [SerializeField]
        private TextMeshProUGUI recoveryPower;

        [SerializeField]
        private TextMeshProUGUI attribute1;

        [SerializeField]
        private TextMeshProUGUI attribute2;

        [SerializeField]
        private TextMeshProUGUI attribute3;


        public void Setup(InstanceEquipment instanceEquipment)
        {
            if (instanceEquipment == null)
            {
                this.root.SetActive(false);
                return;
            }
            
            this.root.SetActive(true);
            var masterData = instanceEquipment.MasterDataEquipment;
            this.equipmentName.text = masterData.localizedName.GetLocalizedString();
            this.hitPoint.text = masterData.hitPoint.ToString();
            this.physicsStrength.text = masterData.physicsStrength.ToString();
            this.physicsDefense.text = masterData.physicsDefense.ToString();
            this.magicStrength.text = masterData.magicStrength.ToString();
            this.magicDefense.text = masterData.magicDefense.ToString();
            this.speed.text = masterData.speed.ToString();
            this.recoveryPower.text = masterData.recoveryPower.ToString();
            this.attribute1.text = masterData.attributeType1.LocalizedText();
            this.attribute2.text = masterData.attributeType2.LocalizedText();
            this.attribute3.text = masterData.attributeType3.LocalizedText();
        }
    }
}
