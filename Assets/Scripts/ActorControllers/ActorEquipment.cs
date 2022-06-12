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

        public List<InstanceEquipmentData> InstanceEquipments => this.instanceEquipments;

        [Serializable]
        public class InstanceEquipmentData
        {
            public Define.EquipmentPartType equipmentPartType;

            public InstanceEquipment instanceEquipment;
        }
    }
}
