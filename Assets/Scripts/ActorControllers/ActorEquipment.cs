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
    public sealed class ActorEquipment
    {
        [SerializeField]
        private List<InstanceEquipmentData> instanceEquipments;

        public List<InstanceEquipmentData> InstanceEquipments => this.instanceEquipments;

        public List<ICommandBlueprintSetupData> CreateCommandBlueprintSetupDataList()
        {
            var result = new List<ICommandBlueprintSetupData>();
            foreach (var i in this.InstanceEquipments)
            {
                var masterDataEquipmentCommands = i.instanceEquipment.MasterDataEquipmentCommands;
            
                // TODO: これだと全部の装備品の合計になっちゃう
                var commandBlueprintHolder = new DebugCommandBlueprintHolder
                {
                    strength = this.InstanceEquipments.Sum(x => x.instanceEquipment.MasterDataEquipment.strength),
                    recoveryPower = this.InstanceEquipments.Sum(x => x.instanceEquipment.MasterDataEquipment.recoveryPower)
                };

                result.AddRange(masterDataEquipmentCommands.Select(r => new DebugCommandBlueprintSetupData
                {
                    blueprintId = r.commandBlueprintId,
                    blueprintHolder = commandBlueprintHolder
                }));
            }

            return result;
        }

        [Serializable]
        public class InstanceEquipmentData
        {
            public Define.EquipmentPartType equipmentPartType;

            public InstanceEquipment instanceEquipment;
        }
    }
}
