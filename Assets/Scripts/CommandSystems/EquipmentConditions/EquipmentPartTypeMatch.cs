using TAKACHIYO.ActorControllers;
using UnityEngine;

namespace TAKACHIYO.CommandSystems.EquipmentConditions
{
    /// <summary>
    /// 指定した部位に装備していたら実行可能
    /// </summary>
    public sealed class EquipmentPartTypeMatch : EquipmentCondition
    {
        [SerializeField]
        private Define.EquipmentPartType target;
        
        public override bool Evaluate(Actor owner, Actor opponent, ICommandBlueprintHolder commandBlueprintHolder)
        {
            return commandBlueprintHolder.EquipmentPartType == target;
        }
    }
}
