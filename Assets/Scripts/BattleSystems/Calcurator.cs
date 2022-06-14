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
        public static int GetAttackDamage(Actor attacker, Actor defenser, Command command, float rate)
        {
            var strength = attacker.StatusController.BaseStatus.physicsStrength + command.BlueprintHolder.PhysicsStrength;
            var defense = 20 + defenser.StatusController.BaseStatus.physicsDefense;

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
            var result = instanceEquipment.MasterDataEquipment.strength;
            
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
                        result += i.instanceEquipment.MasterDataEquipment.strength;
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
