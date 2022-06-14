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
        
        public readonly MasterDataActorStatus.Record BaseStatus;

        private readonly ReactiveProperty<int> hitPointMax;

        private readonly ReactiveProperty<int> hitPoint;

        public string LocalizedName => this.BaseStatus.localizedName.GetLocalizedString();

        public IReadOnlyReactiveProperty<int> HitPointMax => this.hitPointMax;
        
        public IReadOnlyReactiveProperty<int> HitPoint => this.hitPoint;

        public float HitPointRate => (float)this.hitPoint.Value / this.hitPointMax.Value;

        public bool IsDead => this.hitPoint.Value <= 0;

        /// <summary>
        /// 全ての物理防御力を返す
        /// </summary>
        public int TotalPhysicsDefense => this.BaseStatus.physicsDefense + this.owner.Equipment.TotalPhysicsDefense;

        /// <summary>
        /// 全ての魔法防御力を返す
        /// </summary>
        public int TotalMagicDefense => this.BaseStatus.magicDefense + this.owner.Equipment.TotalMagicDefense;

        public int TotalSpeed => this.BaseStatus.speed + this.owner.Equipment.TotalSpeed;
        
        /// <summary>
        /// ダメージを受けた回数
        /// </summary>
        public int TakeDamageCount { get; private set; }

        public ActorStatusController(Actor owner, MasterDataActorStatus.Record masterDataActorStatus)
        {
            this.owner = owner;
            this.BaseStatus = masterDataActorStatus;
            this.hitPointMax = new ReactiveProperty<int>(this.BaseStatus.hitPoint);
            this.hitPoint = new ReactiveProperty<int>(this.BaseStatus.hitPoint);
        }

        public ActorStatusController(Actor owner, string masterDataActorStatusId)
        : this(owner, MasterDataActorStatus.Get(masterDataActorStatusId))
        {
        }

        public void TakeDamage(Actor attacker, int damage, bool isPublishDamageEvent)
        {
            // 鉄壁が存在する場合はダメージ無効化して削除する
            if (this.owner.AbnormalStatusController.Contains(Define.AbnormalStatusType.IronWall))
            {
                damage = 0;
                this.owner.AbnormalStatusController.Remove(Define.AbnormalStatusType.IronWall);
            }
            
            this.TakeDamageRaw(damage);
            
            // 跳返が存在する場合は一部ダメージを相手に与える
            if (damage > 0 && this.owner.AbnormalStatusController.Contains(Define.AbnormalStatusType.Counter))
            {
                var counterDamage = Mathf.FloorToInt(damage * GameDesignParameter.Instance.counterDamageRate);
                attacker.StatusController.TakeDamageRaw(counterDamage);
            }

            // 吸収が存在する場合は一部ダメージを吸収して回復する
            if (damage > 0 && attacker.AbnormalStatusController.Contains(Define.AbnormalStatusType.Absorption))
            {
                var absorptionRecovery = Mathf.FloorToInt(damage * GameDesignParameter.Instance.absorptionRecoveryRate);
                attacker.StatusController.TakeDamageRaw(-absorptionRecovery);
            }

            if (isPublishDamageEvent)
            {
                attacker.Broker.Publish(ActorEvent.GivedDamage.Get());
            }
        }

        public void TakeDamageRaw(int damage)
        {
            if (this.IsDead)
            {
                return;
            }
            
            var result = this.hitPoint.Value;
            result = Mathf.Max(result - damage, 0);
            this.hitPoint.Value = result;

            if (damage < 0)
            {
                this.owner.Broker.Publish(ActorEvent.Recoverd.Get(-damage));
            }
            else if(damage > 0)
            {
                this.TakeDamageCount++;
                this.owner.Broker.Publish(ActorEvent.TakedDamage.Get(damage));
            }
        }

        public void AddHitPointMax(int value)
        {
            this.hitPointMax.Value += value;
        }
    }
}
