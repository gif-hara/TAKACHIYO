using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.CommandSystems
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class CommandBlueprintSetupData
    {
        public string blueprintId;

        public ICommandBlueprintHolder blueprintHolder;
    }
}
