using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.CommandSystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Command
    {
        private CommandBlueprint blueprint;

        private float currentCastTime;
        
        public bool CanInvoke => this.currentCastTime >= this.blueprint.CastTime;

        public Command(CommandBlueprint blueprint)
        {
            this.blueprint = blueprint;
            this.currentCastTime = 0.0f;
        }

        public void Update(float deltaTime)
        {
            this.currentCastTime += deltaTime;
        }

        public void Invoke()
        {
            foreach (var action in this.blueprint.Actions)
            {
                action.Invoke();
            }
        }
    }
}
