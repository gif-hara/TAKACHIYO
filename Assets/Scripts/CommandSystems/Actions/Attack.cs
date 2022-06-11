using System;
using UniRx;
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
        
        public override IObservable<Unit> Invoke(Command command)
        {
            return Observable.Defer(() =>
            {
                foreach (var target in command.Owner.GetTargets(this.targetType))
                {
                    var damage = Mathf.FloorToInt(command.BlueprintHolder.Strength * this.rate);
                    
                    // 脱力にかかっている場合はダメージが減る
                    if (command.Owner.AbnormalStatusController.Contains(Define.AbnormalStatusType.Exhaustion))
                    {
                        damage = Mathf.FloorToInt(damage * GameDesignParameter.Instance.exhaustionDamageRate);
                    }
                    // 怪力にかかっている場合はダメージが増える
                    if (command.Owner.AbnormalStatusController.Contains(Define.AbnormalStatusType.Strong))
                    {
                        damage = Mathf.FloorToInt(damage * GameDesignParameter.Instance.strongDamageRate);
                    }
                    
                    target.StatusController.TakeDamage(
                        command.Owner,
                        damage,
                        true
                        );
                }

                return Observable.ReturnUnit();
            });
        }
    }
}
