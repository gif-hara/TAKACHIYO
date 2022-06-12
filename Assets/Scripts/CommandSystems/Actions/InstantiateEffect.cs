using System;
using TAKACHIYO.ActorControllers;
using UniRx;
using UnityEngine;
using UnityEngine.Search;

namespace TAKACHIYO.CommandSystems.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class InstantiateEffect : CommandAction
    {
        [SerializeField]
        [SearchContext("t:Prefab Effect.")]
        private PoolableEffect effectPrefab;
        
        public override IObservable<Unit> Invoke(Command command)
        {
            return Observable.Defer(() =>
            {
                foreach (var actor in command.Owner.GetTargets(this.targetType))
                {
                    actor.Broker.Publish(ActorEvent.RequestInstantiateEffect.Get(this.effectPrefab));
                }

                return Observable.ReturnUnit();
            });
        }
    }
}
