using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.ActorControllers
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class ActorSetupData
    {
        public string masterDataActorStatusId;

        public List<string> commandBlueprintIds;
    }
}
