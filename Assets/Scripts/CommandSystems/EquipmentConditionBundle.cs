using System.Collections.Generic;
using TAKACHIYO.CommandSystems.EquipmentConditions;
using UnityEngine;

namespace TAKACHIYO.CommandSystems
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "TAKACHIYO/EquipmentConditionBundle")]
    public sealed class EquipmentConditionBundle : ScriptableObject
    {
        [SerializeReference, SubclassSelector(typeof(IEquipmentCondition))]
        private List<IEquipmentCondition> conditions;

        public IReadOnlyList<IEquipmentCondition> Conditions => this.conditions;
    }
}
