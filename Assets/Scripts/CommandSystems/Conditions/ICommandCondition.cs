namespace TAKACHIYO.CommandSystems.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommandCondition
    {
        bool Evaluate(Command command);
    }
}
