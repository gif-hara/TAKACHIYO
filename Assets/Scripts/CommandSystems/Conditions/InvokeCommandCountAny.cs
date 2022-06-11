using TAKACHIYO.ActorControllers;
using UnityEngine;

namespace TAKACHIYO.CommandSystems.Conditions
{
    /// <summary>
    /// 何かコマンドがn回実行したら実行可能
    /// </summary>
    public sealed class InvokeCommandCountAny : CommandCondition
    {
        [SerializeField]
        private int number = 1;
        
        public override bool Evaluate(Command command)
        {
            return (command.Owner.CommandController.InvokedCount - command.LastInvokeOrder) >= this.number;
        }
    }
}
