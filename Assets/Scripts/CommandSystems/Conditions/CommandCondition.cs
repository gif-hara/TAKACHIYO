namespace TAKACHIYO.CommandSystems.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CommandCondition : ICommandCondition
    {
        public abstract bool Evaluate(Command command);
    }
}
