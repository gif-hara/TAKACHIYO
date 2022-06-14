using TAKACHIYO.ActorControllers;
using TAKACHIYO.CommandSystems;
using UnityEngine;

namespace TAKACHIYO.BattleSystems
{
    /// <summary>
    /// バトルの各計算を担うクラス
    /// </summary>
    public static class Calcurator
    {
        /// <summary>
        /// 攻撃した際のダメージ量を返す
        /// </summary>
        public static int GetAttackDamage(Actor attacker, Actor defenser, Command command, float rate, Define.AttackType attackType)
        {
            var strength = attackType == Define.AttackType.Physics
                ? attacker.StatusController.BaseStatus.physicsStrength + command.BlueprintHolder.PhysicsStrength
                : attacker.StatusController.BaseStatus.magicStrength + command.BlueprintHolder.MagicStrength;
            var defense = attackType == Define.AttackType.Magic
                ? 20 + defenser.StatusController.TotalPhysicsDefense
                : 20 + defenser.StatusController.TotalMagicDefense;

            var damage = Mathf.FloorToInt((float)(strength * strength) / defense);
            
            // 脱力にかかっている場合はダメージが減る
            if (attacker.AbnormalStatusController.Contains(Define.AbnormalStatusType.Exhaustion))
            {
                damage = Mathf.FloorToInt(damage * GameDesignParameter.Instance.exhaustionDamageRate);
            }
            
            // 怪力にかかっている場合はダメージが増える
            if (attacker.AbnormalStatusController.Contains(Define.AbnormalStatusType.Strong))
            {
                damage = Mathf.FloorToInt(damage * GameDesignParameter.Instance.strongDamageRate);
            }
            
            // 脆弱にかかっていたらダメージが増加する
            if (defenser.AbnormalStatusController.Contains(Define.AbnormalStatusType.Brittle))
            {
                damage = Mathf.FloorToInt(damage * GameDesignParameter.Instance.brittleDamageRate);
            }
            
            // 頑強にかかっていたらダメージが減少する
            if (defenser.AbnormalStatusController.Contains(Define.AbnormalStatusType.Stubborn))
            {
                damage = Mathf.FloorToInt(damage * GameDesignParameter.Instance.stubbornDamageRate);
            }

            return damage;
        }

        /// <summary>
        /// 攻撃力を返す
        /// </summary>
        public static int GetStrength(
            ActorEquipment actorEquipment,
            InstanceEquipment instanceEquipment,
            Define.EquipmentPartType equipmentPartType
            )
        {
            var result = instanceEquipment.MasterDataEquipment.physicsStrength;
            
            // 防具の場合はこれ以降計算はしない
            if (equipmentPartType.IsArmor())
            {
                return result;
            }
            
            // 武器の場合はさらに防具に存在する攻撃力を加算する
            if (equipmentPartType.IsWeapon())
            {
                foreach (var i in actorEquipment.InstanceEquipments)
                {
                    if (i.equipmentPartType.IsArmor())
                    {
                        result += i.instanceEquipment.MasterDataEquipment.physicsStrength;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 回復力を返す
        /// </summary>
        public static int GetRecoveryPower(
            ActorEquipment actorEquipment,
            InstanceEquipment instanceEquipment,
            Define.EquipmentPartType equipmentPartType
            )
        {
            var result = instanceEquipment.MasterDataEquipment.recoveryPower;
            
            // 防具の場合はこれ以降計算はしない
            if (equipmentPartType.IsArmor())
            {
                return result;
            }
            
            // 武器の場合はさらに防具に存在する攻撃力を加算する
            if (equipmentPartType.IsWeapon())
            {
                foreach (var i in actorEquipment.InstanceEquipments)
                {
                    if (i.equipmentPartType.IsArmor())
                    {
                        result += i.instanceEquipment.MasterDataEquipment.recoveryPower;
                    }
                }
            }

            return result;
        }
    }
}
