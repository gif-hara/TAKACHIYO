using System;
using System.Collections.Generic;
using TAKACHIYO.CommandSystems.Conditions;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.CommandSystems
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class CommandConditionBundle
    {
        [SerializeReference, SubclassSelector(typeof(ICommandCondition))]
        private List<ICommandCondition> conditions;

        public IReadOnlyList<ICommandCondition> Conditions => this.conditions;
    }
}
