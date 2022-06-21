using System.Linq;
using TAKACHIYO.ActorControllers;
using UnityEngine;
using UnityEngine.Localization;

namespace TAKACHIYO.CommandSystems.EquipmentConditions
{
    /// <summary>
    /// 相手に指定した属性があれば実行可能
    /// </summary>
    public sealed class OpponentAttributeMatch : EquipmentCondition
    {
        [SerializeField]
        private Define.AttributeType attributeType;
        
        public override bool Evaluate(Actor owner, Actor opponent, ICommandBlueprintHolder commandBlueprintHolder)
        {
            return opponent.StatusController.BaseStatus.attributeTypes.Any(x => x == attributeType);
        }
        
        public override string LocalizedDescription
        {
            get
            {
                return string.Format(
                    new LocalizedString("Common", "Condition.OpponentAttributeMatch").GetLocalizedString(),
                    this.attributeType.LocalizedString()
                    );
            }
        }
    }
}
