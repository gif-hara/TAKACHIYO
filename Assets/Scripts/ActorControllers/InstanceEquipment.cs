using System;
using System.Collections.Generic;
using System.Linq;
using TAKACHIYO.CommandSystems;
using TAKACHIYO.MasterDataSystems;
using UnityEngine;
using UnityEngine.Assertions;

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

        public MasterDataEquipment.Record MasterDataEquipment => MasterDataSystems.MasterDataEquipment.Get(this.masterDataEquipmentId);
        
        public InstanceEquipment(string masterDataEquipmentId)
        {
            this.masterDataEquipmentId = masterDataEquipmentId;
        }
        
        public IEnumerable<ICommandBlueprintSetupData> CreateCommandBlueprintSetupDataList(ActorEquipment actorEquipment)
        { 
            var masterDataEquipmentCommands = MasterDataEquipmentCommand.GetFromEquipmentId(this.masterDataEquipmentId);
            var commandBlueprintHolder = new DebugCommandBlueprintHolder
            {
                strength = actorEquipment.InstanceEquipments.Sum(x => x.instanceEquipment.MasterDataEquipment.strength),
                recoveryPower = actorEquipment.InstanceEquipments.Sum(x => x.instanceEquipment.MasterDataEquipment.recoveryPower)
            };

            return masterDataEquipmentCommands.Select(r => new DebugCommandBlueprintSetupData
                {
                    blueprintId = r.commandBlueprintId,
                    blueprintHolder = commandBlueprintHolder
                });
        }
    }
}
