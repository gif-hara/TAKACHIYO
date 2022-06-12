using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.ActorControllers
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class ActorEquipment
    {
        [SerializeField]
        private List<InstanceEquipmentData> instanceEquipments;

        [Serializable]
        private class InstanceEquipmentData
        {
            public Define.EquipmentType equipmentType;

            public InstanceEquipment instanceEquipment;
        }
    }
}
