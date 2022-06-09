using TAKACHIYO.ActorControllers;
using UnityEngine;

namespace TAKACHIYO.CommandSystems.Conditions
{
    /// <summary>
    /// 何かコマンドがn回実行したら実行可能
    /// </summary>
    public sealed class InvokeCommandCount : CommandCondition
    {
        [SerializeField]
        private Define.TargetType targetType;

        [SerializeField]
        private int number = 1;
        
        public override bool Evaluate(ActorCommandController commandController, Command command)
        {
            return (commandController.InvokedCount - command.LastInvokeOrder) >= this.number;
        }
    }
}
