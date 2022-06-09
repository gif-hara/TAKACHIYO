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

        public bool IsDead => this.hitPoint.Value <= 0;

        public ActorStatusController(MasterDataActorStatus.Record masterDataActorStatus)
        {
            this.baseStatus = masterDataActorStatus;
            this.hitPointMax = new ReactiveProperty<int>(this.baseStatus.hitPoint);
            this.hitPoint = new ReactiveProperty<int>(this.baseStatus.hitPoint);
        }

        public ActorStatusController(string masterDataActorStatusId)
        : this(MasterDataActorStatus.Get(masterDataActorStatusId))
        {
            
        }

        public void TakeDamage(int damage)
        {
            if (this.IsDead)
            {
                return;
            }

            var result = this.hitPoint.Value;
            result = Mathf.Max(result - damage, 0);
            this.hitPoint.Value = result;
        }
    }
}
