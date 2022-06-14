using TAKACHIYO.ActorControllers;

namespace TAKACHIYO.CommandSystems.EquipmentConditions
{
    /// <summary>
    /// 装備品によるコマンドの実行条件を持つインターフェイス
    /// </summary>
    public interface IEquipmentCondition
    {
        bool Evaluate(Actor owner, Actor opponent, ICommandBlueprintHolder commandBlueprintHolder);
    }
}
