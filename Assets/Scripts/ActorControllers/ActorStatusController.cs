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
        private Actor owner;
        
        private readonly MasterDataActorStatus.Record baseStatus;

        private readonly ReactiveProperty<int> hitPointMax;

        private readonly ReactiveProperty<int> hitPoint;

        public string LocalizedName => this.baseStatus.localizedName.GetLocalizedString();

        public IReadOnlyReactiveProperty<int> HitPointMax => this.hitPointMax;
        
        public IReadOnlyReactiveProperty<int> HitPoint => this.hitPoint;

        public float HitPointRate => (float)this.hitPoint.Value / this.hitPointMax.Value;

        public bool IsDead => this.hitPoint.Value <= 0;
        
        /// <summary>
        /// ダメージを受けた回数
        /// </summary>
        public int TakeDamageCount { get; private set; }

        public ActorStatusController(Actor owner, MasterDataActorStatus.Record masterDataActorStatus)
        {
            this.owner = owner;
            this.baseStatus = masterDataActorStatus;
            this.hitPointMax = new ReactiveProperty<int>(this.baseStatus.hitPoint);
            this.hitPoint = new ReactiveProperty<int>(this.baseStatus.hitPoint);
        }

        public ActorStatusController(Actor owner, string masterDataActorStatusId)
        : this(owner, MasterDataActorStatus.Get(masterDataActorStatusId))
        {
        }

        public void TakeDamage(Actor attacker, int damage, bool isPublishDamageEvent)
        {
            this.TakeDamageRaw(damage);
            
            if (isPublishDamageEvent)
            {
                this.owner.Broker.Publish(ActorEvent.TakedDamage.Get(damage));
                attacker.Broker.Publish(ActorEvent.GivedDamage.Get());
            }
        }

        public void TakeDamageRaw(int damage)
        {
            if (this.IsDead)
            {
                return;
            }

            // 脆弱にかかっていたらダメージが増加する
            if (this.owner.AbnormalStatusController.Contains(Define.AbnormalStatusType.Brittle))
            {
                damage = Mathf.FloorToInt(damage * GameDesignParameter.Instance.brittleDamageRate);
            }
            
            // 頑強にかかっていたらダメージが減少する
            if (this.owner.AbnormalStatusController.Contains(Define.AbnormalStatusType.Stubborn))
            {
                damage = Mathf.FloorToInt(damage * GameDesignParameter.Instance.stubbornDamageRate);
            }
            
            this.TakeDamageCount++;
            var result = this.hitPoint.Value;
            result = Mathf.Max(result - damage, 0);
            this.hitPoint.Value = result;
        }
    }
}
