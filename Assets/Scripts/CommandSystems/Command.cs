using TAKACHIYO.ActorControllers;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.CommandSystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Command
    {
        private readonly CommandBlueprint blueprint;

        private readonly ReactiveProperty<float> currentCastTime;

        private Actor owner;

        public string CommandName => this.blueprint.CommandName;
        
        public bool CanInvoke => this.currentCastTime.Value >= this.blueprint.CastTime;

        public float CastTimeRate => this.currentCastTime.Value / this.blueprint.CastTime;

        public IReadOnlyReactiveProperty<float> CurrentCastTime => this.currentCastTime;

        public Command(CommandBlueprint blueprint, Actor owner)
        {
            this.blueprint = blueprint;
            this.owner = owner;
            this.currentCastTime = new ReactiveProperty<float>();
        }

        public void Reset()
        {
            this.currentCastTime.Value = 0.0f;
        }

        public void Update(float deltaTime)
        {
            this.currentCastTime.Value += deltaTime;
        }

        public void Invoke()
        {
            foreach (var action in this.blueprint.Actions)
            {
                action.Invoke(this.owner);
            }
        }
    }
}
