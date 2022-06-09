using TAKACHIYO.ActorControllers;

namespace TAKACHIYO.CommandSystems.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommandCondition
    {
        bool Evaluate(ActorCommandController commandController, Command command);
    }
}
