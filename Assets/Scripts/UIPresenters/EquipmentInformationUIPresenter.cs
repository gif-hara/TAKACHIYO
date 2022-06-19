using Cysharp.Threading.Tasks;
using HK.Framework;
using TAKACHIYO.ActorControllers;
using TAKACHIYO.CommandSystems;
using TAKACHIYO.MasterDataSystems;
using TMPro;
using UnityEngine;

namespace TAKACHIYO.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EquipmentInformationUIPresenter : AnimatableUIPresenter
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

        [SerializeField]
        private Transform commandUIViewRoot;
        
        [SerializeField]
        private EquipmentInformationCommandUIView commandUIView;

        private ObjectPool<EquipmentInformationCommandUIView> commandUIViewPool;

        public override UniTask UIInitialize()
        {
            this.commandUIViewPool = new ObjectPool<EquipmentInformationCommandUIView>(this.commandUIView);
            return base.UIInitialize();
        }
        
        public async UniTask SetupAsync(InstanceEquipment instanceEquipment)
        {
            if (instanceEquipment == null)
            {
                this.root.SetActive(false);
                return;
            }
            
            this.root.SetActive(true);
            var masterDataEquipment = instanceEquipment.MasterDataEquipment;
            this.equipmentName.text = masterDataEquipment.localizedName.GetLocalizedString();
            this.hitPoint.text = masterDataEquipment.hitPoint.ToString();
            this.physicsStrength.text = masterDataEquipment.physicsStrength.ToString();
            this.physicsDefense.text = masterDataEquipment.physicsDefense.ToString();
            this.magicStrength.text = masterDataEquipment.magicStrength.ToString();
            this.magicDefense.text = masterDataEquipment.magicDefense.ToString();
            this.speed.text = masterDataEquipment.speed.ToString();
            this.recoveryPower.text = masterDataEquipment.recoveryPower.ToString();
            this.attribute1.text = masterDataEquipment.attributeType1.LocalizedText();
            this.attribute2.text = masterDataEquipment.attributeType2.LocalizedText();
            this.attribute3.text = masterDataEquipment.attributeType3.LocalizedText();
            
            this.commandUIViewPool.ReturnAll();
            var masterDataEquipmentCommands = MasterDataEquipmentCommand.GetFromEquipmentId(masterDataEquipment.Id);
            foreach (var i in masterDataEquipmentCommands)
            {
                var commandUIView = this.commandUIViewPool.Rent();
                commandUIView.transform.SetParent(this.commandUIViewRoot, false);
                var result = await AssetLoader.LoadAsyncTask<CommandBlueprint>($"Assets/DataSources/CommandBlueprint/CommandBlueprint.{i.commandBlueprintId}.asset");
                commandUIView.CommandName = result.CommandName;
                commandUIView.CastTime = result.CastTime.ToString("0.00s");
            }
        }
    }
}