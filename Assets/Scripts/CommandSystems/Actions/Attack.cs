using TAKACHIYO.ActorControllers;
using UnityEngine;

namespace TAKACHIYO.CommandSystems.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Attack : CommandAction
    {
        [SerializeField]
        private float rate = 1.0f;
        
        public override void Invoke(Actor owner)
        {
            foreach (var target in owner.GetTargets(this.targetType))
            {
                target.StatusController.TakeDamage(25);
            }
        }
    }
}
