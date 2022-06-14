namespace TAKACHIYO.CommandSystems
{
    /// <summary>
    /// CommandBlueprintを持つインターフェイス
    /// </summary>
    /// <remarks>
    /// 主に武器などがこのインターフェイスを継承しています
    /// </remarks>
    public interface ICommandBlueprintHolder
    {
        /// <summary>
        /// 物理攻撃力
        /// </summary>
        int PhysicsStrength { get; }
        
        /// <summary>
        /// 回復力
        /// </summary>
        int RecoveryPower { get; }
    }
}
