using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Localization;

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

        public static string LocalizedText(this Define.AttributeType self)
        {
            return new LocalizedString("Common", $"AttributeType.{(int)self:00}").GetLocalizedString();
        }

        /// <summary>
        /// <paramref name="self"/>に<paramref name="equipmentType"/>が装備可能か返す
        /// </summary>
        public static bool CanEquip(this Define.EquipmentPartType self, Define.EquipmentType equipmentType)
        {
            switch (self)
            {
                case Define.EquipmentPartType.MainWeapon:
                case Define.EquipmentPartType.SubWeapon1:
                case Define.EquipmentPartType.SubWeapon2:
                    return equipmentType == Define.EquipmentType.Weapon;
                case Define.EquipmentPartType.ArmorHead:
                    return equipmentType == Define.EquipmentType.ArmorHead;
                case Define.EquipmentPartType.ArmorChest:
                    return equipmentType == Define.EquipmentType.ArmorChest;
                case Define.EquipmentPartType.ArmorArms:
                    return equipmentType == Define.EquipmentType.ArmorArms;
                case Define.EquipmentPartType.ArmorTorso:
                    return equipmentType == Define.EquipmentType.ArmorTorso;
                case Define.EquipmentPartType.ArmorLegs:
                    return equipmentType == Define.EquipmentType.ArmorLegs;
                case Define.EquipmentPartType.Accessory:
                    return equipmentType == Define.EquipmentType.Accessory;
                default:
                    Assert.IsTrue(false, $"{self}は未対応です");
                    return default;
            }
        }
    }
}
