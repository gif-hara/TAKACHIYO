using TAKACHIYO.ActorControllers;
using UnityEngine;

namespace TAKACHIYO.CommandSystems.Conditions
{
    /// <summary>
    /// ダメージをn回受けたら実行可能
    /// </summary>
    public sealed class TakeDamageCount : CommandCondition
    {
        [SerializeField]
        private int number = 1;
        
        public override bool Evaluate(Command command)
        {
            return (command.Owner.StatusController.TakeDamageCount - command.LastTakeDamageCount) >= this.number;
        }
    }
}
