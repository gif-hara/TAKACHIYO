using TAKACHIYO.ActorControllers;

namespace TAKACHIYO.CommandSystems.EquipmentConditions
{
    /// <summary>
    /// 常に実行可能
    /// </summary>
    public sealed class Always : EquipmentCondition
    {
        public override bool Evaluate(Actor owner, Actor opponent, ICommandBlueprintHolder commandBlueprintHolder)
        {
            return true;
        }
    }
}