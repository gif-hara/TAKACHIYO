using TAKACHIYO.ActorControllers;
using UnityEngine;

namespace TAKACHIYO.CommandSystems.EquipmentConditions
{
    /// <summary>
    /// サブ武器を装備せず、メイン武器のみ装備していたら実行可能
    /// </summary>
    public sealed class WeaponMainOnly : EquipmentCondition
    {
        public override bool Evaluate(Actor owner, Actor opponent, ICommandBlueprintHolder commandBlueprintHolder)
        {
            var e = owner.Equipment;
            return e.GetOrNull(Define.EquipmentPartType.SubWeapon1) == null && e.GetOrNull(Define.EquipmentPartType.SubWeapon2) == null;
        }
    }
}
