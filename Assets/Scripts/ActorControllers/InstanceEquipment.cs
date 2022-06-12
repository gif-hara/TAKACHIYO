using System;
using System.Collections.Generic;
using TAKACHIYO.MasterDataSystems;
using UnityEngine;

namespace TAKACHIYO.ActorControllers
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class InstanceEquipment
    {
        [SerializeField]
        private string masterDataEquipmentId;

        public IReadOnlyList<MasterDataEquipmentCommand.Record> MasterDataEquipmentCommands => MasterDataEquipmentCommand.GetFromEquipmentId(this.masterDataEquipmentId);

        public MasterDataEquipment.Record MasterDataEquipment => MasterDataSystems.MasterDataEquipment.Get(this.masterDataEquipmentId);
        
        public InstanceEquipment(string masterDataEquipmentId)
        {
            this.masterDataEquipmentId = masterDataEquipmentId;
        }
    }
}
