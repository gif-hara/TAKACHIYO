using System;
using System.Collections.Generic;
using TAKACHIYO.CommandSystems.CommandConditions;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.CommandSystems
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "TAKACHIYO/CommandConditionBundle")]
    public sealed class CommandConditionBundle : ScriptableObject
    {
        [SerializeReference, SubclassSelector(typeof(ICommandCondition))]
        private List<ICommandCondition> conditions;

        public IReadOnlyList<ICommandCondition> Conditions => this.conditions;
    }
}
