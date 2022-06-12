using System;
using System.Collections.Generic;
using TAKACHIYO.CommandSystems;

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

        public IEnumerable<ICommandBlueprintSetupData> CommandBlueprintSetupData => this.actorEquipment.CreateCommandBlueprintSetupDataList();
    }
}
