using TAKACHIYO.ActorControllers;
using UnityEngine;
using UnityEngine.Localization;

namespace TAKACHIYO.CommandSystems.EquipmentConditions
{
    /// <summary>
    /// 指定した部位に装備していたら実行可能
    /// </summary>
    public sealed class EquipmentScopeTypeMatch : EquipmentCondition
    {
        [SerializeField]
        private Define.EquipmentScopeType target;
        
        public override bool Evaluate(Actor owner, Actor opponent, ICommandBlueprintHolder commandBlueprintHolder)
        {
            return this.target.IsMatch(commandBlueprintHolder.EquipmentPartType);
        }
        
        public override string LocalizedDescription
        {
            get
            {
                return string.Format(
                    new LocalizedString("Common", "Condition.EquipmentScopeTypeMatch").GetLocalizedString(),
                    this.target.LocalizedString()
                    );
            }
        }
    }
}
