namespace TAKACHIYO.CommandSystems.Conditions
{
    /// <summary>
    /// 常に実行可能なコマンド条件
    /// </summary>
    public sealed class Always : CommandCondition
    {
        public override bool Evaluate(Command command)
        {
            return true;
        }
    }
}
