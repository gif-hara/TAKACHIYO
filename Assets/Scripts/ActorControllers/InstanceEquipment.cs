using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.ActorControllers
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class InstanceEquipment
    {
        [SerializeField]
        private string masterDataEquipmentId;
        
        public InstanceEquipment(string masterDataEquipmentId)
        {
            this.masterDataEquipmentId = masterDataEquipmentId;
        }
    }
}
