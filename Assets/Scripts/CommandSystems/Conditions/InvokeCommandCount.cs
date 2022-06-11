using TAKACHIYO.ActorControllers;
using UnityEngine;

namespace TAKACHIYO.CommandSystems.Conditions
{
    /// <summary>
    /// n回だけ実行可能
    /// </summary>
    public sealed class InvokeCommandCount : CommandCondition
    {
        [SerializeField]
        private int number = 1;
        
        public override bool Evaluate(Command command)
        {
            return command.InvokedCount < this.number;
        }
    }
}
