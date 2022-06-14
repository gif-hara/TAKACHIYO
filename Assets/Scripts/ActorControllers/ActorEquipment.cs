using System;
using System.Collections.Generic;
using System.Linq;
using TAKACHIYO.BattleSystems;
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
        private List<InstanceEquipmentData> instanceEquipments = new();

        public List<InstanceEquipmentData> InstanceEquipments => this.instanceEquipments;

        /// <summary>
        /// 総合物理防御力を返す
        /// </summary>
        /// <remarks>
        /// 全ての物理防御力を参照したい場合は<see cref="ActorStatusController.TotalPhysicsDefense"/>を参照してください
        /// </remarks>
        public int TotalPhysicsDefense => this.InstanceEquipments.Sum(x => x.instanceEquipment.MasterDataEquipment.defense);

        public List<ICommandBlueprintSetupData> CreateCommandBlueprintSetupDataList()
        {
            var result = new List<ICommandBlueprintSetupData>();
            foreach (var i in this.InstanceEquipments)
            {
                var masterDataEquipmentCommands = i.instanceEquipment.MasterDataEquipmentCommands;
            
                var commandBlueprintHolder = new DebugCommandBlueprintHolder
                {
                    physicsStrength = Calcurator.GetStrength(this, i.instanceEquipment, i.equipmentPartType),
                    recoveryPower = Calcurator.GetRecoveryPower(this, i.instanceEquipment, i.equipmentPartType)
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
