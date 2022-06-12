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
    }
}
