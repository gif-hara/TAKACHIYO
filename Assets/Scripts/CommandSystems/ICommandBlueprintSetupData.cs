namespace TAKACHIYO.CommandSystems
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommandBlueprintSetupData
    {
        public string BlueprintId { get; }

        public ICommandBlueprintHolder BlueprintHolder { get; }
    }
}
