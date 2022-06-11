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
        public static int GetDamage(Actor attacker, Actor defenser, Command command, float rate)
        {
            var strength = attacker.StatusController.BaseStatus.strength + command.BlueprintHolder.Strength;
            var defense = 20 + defenser.StatusController.BaseStatus.defense;

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
    }
}
