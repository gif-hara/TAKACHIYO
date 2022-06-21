using TAKACHIYO.ActorControllers;

namespace TAKACHIYO.CommandSystems.EquipmentConditions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class EquipmentCondition : IEquipmentCondition
    {
        public abstract bool Evaluate(Actor owner, Actor opponent, ICommandBlueprintHolder commandBlueprintHolder);

        public abstract string LocalizedDescription { get; }
    }
}
