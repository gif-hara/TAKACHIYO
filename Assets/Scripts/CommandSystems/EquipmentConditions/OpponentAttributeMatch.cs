using System.Linq;
using TAKACHIYO.ActorControllers;
using UnityEngine;

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
    }
}
