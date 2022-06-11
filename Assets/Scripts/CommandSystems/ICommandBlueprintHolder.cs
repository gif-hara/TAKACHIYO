namespace TAKACHIYO.CommandSystems
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommandBlueprintHolder
    {
        /// <summary>
        /// 攻撃力
        /// </summary>
        int Strength { get; }
        
        /// <summary>
        /// 回復力
        /// </summary>
        int RecoveryPower { get; }
    }
}
