using System.Collections.Generic;
using TAKACHIYO.CommandSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.ActorControllers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IActorSetupData
    {
        public string MasterDataActorStatusId { get; }

        public IEnumerable<ICommandBlueprintSetupData> CommandBlueprintSetupData { get; }
    }
}
