using System;
using TAKACHIYO.ActorControllers;
using TAKACHIYO.BattleSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Localization;

namespace TAKACHIYO.CommandSystems.CommandConditions
{
    /// <summary>
    /// n回だけ実行可能
    /// </summary>
    public sealed class InvokeCommandCount : CommandCondition
    {
        [SerializeField]
        private int number;

        public override IObservable<Unit> TryCastAsObservable(Actor owner)
        {
            return Observable.Defer(() =>
            {
                return Observable.Merge(
                    BattleSceneController.Broker.Receive<BattleEvent.StartBattle>().AsUnitObservable(),
                    owner.Broker.Receive<ActorEvent.InvokedCommand>().AsUnitObservable()
                    );
            });
        }
        
        public override bool Evaluate(Command command)
        {
            if (this.number <= 0)
            {
                return true;
            }
            
            return command.InvokedCount < this.number;
        }
        
                
        public override string LocalizedDescription
        {
            get
            {
                if (this.number <= 0)
                {
                    return new LocalizedString("Common", "Condition.InvokeCommandCount.Always").GetLocalizedString();
                }
                else
                {
                    return string.Format(
                        new LocalizedString("Common", "Condition.InvokeCommandCount.Number").GetLocalizedString(),
                        this.number
                        );

                }
            }
        }
    }
}
