using System.Collections.Generic;
using TAKACHIYO.CommandSystems.Actions;
using TAKACHIYO.CommandSystems.CommandConditions;
using TAKACHIYO.CommandSystems.EquipmentConditions;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Localization;
using UnityEngine.Serialization;

namespace TAKACHIYO.CommandSystems
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "TAKACHIYO/CommandBlueprint")]
    public sealed class CommandBlueprint : ScriptableObject
    {
        [SerializeField]
        private EquipmentConditionBundle equipmentConditionBundle;
        
        [SerializeField]
        private CommandConditionBundle commandConditionBundle;
        
        [SerializeField]
        private CommandActionBundle actionBundle;

        public string CommandName => this.actionBundle.CommandName;

        public float CastTime => this.actionBundle.CastTime;

        public IReadOnlyList<IEquipmentCondition> EquipmentConditions => this.equipmentConditionBundle.Conditions;
        
        public IReadOnlyList<ICommandCondition> CommandConditions => this.commandConditionBundle.Conditions;

        public IReadOnlyList<ICommandAction> Actions => this.actionBundle.Actions;
    }
}
