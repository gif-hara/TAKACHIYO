using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.CommandSystems
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class DebugCommandBlueprintSetupData : ICommandBlueprintSetupData
    {
        public string blueprintId;

        public DebugCommandBlueprintHolder blueprintHolder;

        public string BlueprintId => this.blueprintId;

        public ICommandBlueprintHolder BlueprintHolder => this.blueprintHolder;
    }
}
