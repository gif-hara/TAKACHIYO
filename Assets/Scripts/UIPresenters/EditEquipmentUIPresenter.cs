using HK.Framework;
using TAKACHIYO.BootSystems;
using TAKACHIYO.SaveData;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EditEquipmentUIPresenter : AnimatableUIPresenter
    {
        [SerializeField]
        private Transform currentEquipmentParent;

        [SerializeField]
        private EditEquipmentButtonUIView editEquipmentButtonPrefab;
    }
}
