using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumExtensions
    {
        public static bool IsWeapon(this Define.EquipmentPartType self)
        {
            switch (self)
            {
                case Define.EquipmentPartType.MainWeapon:
                case Define.EquipmentPartType.SubWeapon1:
                case Define.EquipmentPartType.SubWeapon2:
                    return true;
                case Define.EquipmentPartType.ArmorHead:
                case Define.EquipmentPartType.ArmorChest:
                case Define.EquipmentPartType.ArmorArms:
                case Define.EquipmentPartType.ArmorTorso:
                case Define.EquipmentPartType.ArmorLegs:
                case Define.EquipmentPartType.Accessory:
                    return false;
                default:
                    Assert.IsTrue(false, $"{self}は未対応です");
                    return default;
            }
        }
        
        public static bool IsArmor(this Define.EquipmentPartType self)
        {
            switch (self)
            {
                case Define.EquipmentPartType.MainWeapon:
                case Define.EquipmentPartType.SubWeapon1:
                case Define.EquipmentPartType.SubWeapon2:
                    return false;
                case Define.EquipmentPartType.ArmorHead:
                case Define.EquipmentPartType.ArmorChest:
                case Define.EquipmentPartType.ArmorArms:
                case Define.EquipmentPartType.ArmorTorso:
                case Define.EquipmentPartType.ArmorLegs:
                case Define.EquipmentPartType.Accessory:
                    return true;
                default:
                    Assert.IsTrue(false, $"{self}は未対応です");
                    return default;
            }
        }
        
        /// <summary>
        /// スコープと部位が一致しているか返す
        /// </summary>
        public static bool IsMatch(this Define.EquipmentScopeType self, Define.EquipmentPartType partType)
        {
            switch (self)
            {
                case Define.EquipmentScopeType.Main:
                    return partType == Define.EquipmentPartType.MainWeapon;
                case Define.EquipmentScopeType.Sub:
                    return partType == Define.EquipmentPartType.SubWeapon1
                        || partType == Define.EquipmentPartType.SubWeapon2;
                case Define.EquipmentScopeType.Armor:
                    return partType == Define.EquipmentPartType.ArmorHead
                        || partType == Define.EquipmentPartType.ArmorChest
                        || partType == Define.EquipmentPartType.ArmorArms
                        || partType == Define.EquipmentPartType.ArmorTorso
                        || partType == Define.EquipmentPartType.ArmorLegs;
                case Define.EquipmentScopeType.Accessory:
                    return partType == Define.EquipmentPartType.Accessory;
                case Define.EquipmentScopeType.All:
                    return true;
                default:
                    Assert.IsTrue(false, $"{self}は未対応です");
                    return default;
            }
        }
    }
}
