using System.Collections.Generic;
using TAKACHIYO.CommandSystems.Actions;
using TAKACHIYO.CommandSystems.Conditions;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Localization;

namespace TAKACHIYO.CommandSystems
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "TAKACHIYO/CommandBlueprint")]
    public sealed class CommandBlueprint : ScriptableObject
    {
        [SerializeField]
        private CommandConditionBundle conditionBundle;
        
        [SerializeField]
        private CommandActionBundle actionBundle;

        public string CommandName => this.actionBundle.CommandName;

        public float CastTime => this.actionBundle.CastTime;

        public IReadOnlyList<ICommandCondition> Conditions => this.conditionBundle.Conditions;

        public IReadOnlyList<ICommandAction> Actions => this.actionBundle.Actions;
    }
}
