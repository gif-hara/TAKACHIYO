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
    public sealed class DebugActorSetupData : IActorSetupData
    {
        public string masterDataActorStatusId;

        public List<DebugCommandBlueprintSetupData> commandBlueprintSetupData;

        public string MasterDataActorStatusId => this.masterDataActorStatusId;
        
        public IEnumerable<ICommandBlueprintSetupData> CommandBlueprintSetupData => this.commandBlueprintSetupData;
    }
}
