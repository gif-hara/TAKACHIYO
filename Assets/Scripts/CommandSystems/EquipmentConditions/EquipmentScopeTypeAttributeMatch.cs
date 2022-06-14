using System.Linq;
using TAKACHIYO.ActorControllers;
using UnityEngine;

namespace TAKACHIYO.CommandSystems.EquipmentConditions
{
    /// <summary>
    /// 指定したスコープに属性が存在していたら実行可能
    /// </summary>
    public sealed class EquipmentScopeTypeAttributeMatch : EquipmentCondition
    {
        [SerializeField]
        private Define.EquipmentScopeType equipmentScopeType;

        [SerializeField]
        private Define.AttributeType attributeType;

        [SerializeField]
        private int number = 1;
        
        public override bool Evaluate(Actor owner, Actor opponent, ICommandBlueprintHolder commandBlueprintHolder)
        {
            var count = owner.Equipment
                .Get(this.equipmentScopeType)
                .Sum(x => x.MasterDataEquipment.attributeTypes.Count(attribute => attribute == this.attributeType));

            return count >= this.number;
        }
    }
}
