using System;
using System.Collections.Generic;
using TAKACHIYO.ActorControllers.AbnormalStatuses;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.ActorControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ActorAbnormalStatusController
    {
        private readonly Actor owner;
        
        private readonly Dictionary<Define.AbnormalStatusType, IAbnormalStatus> abnormalStatuses = new();

        public ActorAbnormalStatusController(Actor owner)
        {
            this.owner = owner;
        }

        public void Add(Define.AbnormalStatusType abnormalStatusType)
        {
            if (this.abnormalStatuses.ContainsKey(abnormalStatusType))
            {
                return;
            }

            var abnormalStatus = CreateAbnormalStatus(abnormalStatusType);
            abnormalStatus.Setup(this.owner);
            this.abnormalStatuses.Add(abnormalStatusType, abnormalStatus);
        }

        public void Remove(Define.AbnormalStatusType abnormalStatusType)
        {
            Assert.IsTrue(this.abnormalStatuses.ContainsKey(abnormalStatusType), $"{abnormalStatusType}は存在しません");
            this.abnormalStatuses[abnormalStatusType].Dispose();
            this.abnormalStatuses.Remove(abnormalStatusType);
        }

        public bool Contains(Define.AbnormalStatusType abnormalStatusType)
        {
            return this.abnormalStatuses.ContainsKey(abnormalStatusType);
        }

        private static IAbnormalStatus CreateAbnormalStatus(Define.AbnormalStatusType abnormalStatusType)
        {
            switch (abnormalStatusType)
            {
                case Define.AbnormalStatusType.Poison:
                    return new Poison();
                case Define.AbnormalStatusType.Paralysis:
                    return new TimerAbnormalStatus(Define.AbnormalStatusType.Paralysis, GameDesignParameter.Instance.ParalysisTimeSeconds);
                case Define.AbnormalStatusType.Sleep:
                case Define.AbnormalStatusType.Exhaustion:
                case Define.AbnormalStatusType.Brittle:
                case Define.AbnormalStatusType.Trip:
                case Define.AbnormalStatusType.Healing:
                case Define.AbnormalStatusType.FleetSpeed:
                case Define.AbnormalStatusType.Strong:
                case Define.AbnormalStatusType.Stubborn:
                default:
                    Assert.IsTrue(false, $"{abnormalStatusType}は未対応です");
                    return null;
            }
        }
    }
}