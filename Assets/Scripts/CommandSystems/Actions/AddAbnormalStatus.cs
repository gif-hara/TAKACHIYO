using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace TAKACHIYO.CommandSystems.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AddAbnormalStatus : CommandAction
    {
        [SerializeField]
        private List<Define.AbnormalStatusType> abnormalStatusTypes;
        
        public override IObservable<Unit> Invoke(Command command)
        {
            return Observable.Defer(() =>
            {
                foreach (var target in command.Owner.GetTargets(this.targetType))
                {
                    foreach (var abnormalStatusType in this.abnormalStatusTypes)
                    {
                        target.AbnormalStatusController.Add(abnormalStatusType);
                    }
                }

                return Observable.ReturnUnit();
            });
        }
    }
}