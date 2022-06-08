using TAKACHIYO.MasterDataSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.ActorControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ActorStatusController
    {
        private readonly MasterDataActorStatus.Record baseStatus;

        private readonly ReactiveProperty<int> hitPointMax;

        private readonly ReactiveProperty<int> hitPoint;

        public string LocalizedName => this.baseStatus.localizedName.GetLocalizedString();

        public IReadOnlyReactiveProperty<int> HitPointMax => this.hitPointMax;
        
        public IReadOnlyReactiveProperty<int> HitPoint => this.hitPoint;

        public float HitPointRate => (float)this.hitPoint.Value / this.hitPointMax.Value;

        public ActorStatusController(MasterDataActorStatus.Record masterDataActorStatus)
        {
            this.baseStatus = masterDataActorStatus;
            this.hitPointMax = new ReactiveProperty<int>(this.baseStatus.hitPoint);
            this.hitPoint = new ReactiveProperty<int>(this.baseStatus.hitPoint);
        }
    }
}
