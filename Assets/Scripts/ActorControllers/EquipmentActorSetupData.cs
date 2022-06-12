using System;
using System.Collections.Generic;
using System.Linq;
using TAKACHIYO.CommandSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.ActorControllers
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class EquipmentActorSetupData : IActorSetupData
    {
        public string masterDataActorStatusId;

        public ActorEquipment actorEquipment;

        public string MasterDataActorStatusId => this.masterDataActorStatusId;

        public IEnumerable<ICommandBlueprintSetupData> CommandBlueprintSetupData
        {
            get
            {
                var result = new List<ICommandBlueprintSetupData>();
                foreach (var i in this.actorEquipment.InstanceEquipments)
                {
                    result.AddRange(i.instanceEquipment.CreateCommandBlueprintSetupDataList(this.actorEquipment));
                }

                return result;
            }
        }
    }
}
