using System;
using System.Collections.Generic;
using System.Linq;
using TAKACHIYO.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.SaveData
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class UserData
    {
        public static UserData Instance { get; set; }
        
        [SerializeField]
        private string name;

        [SerializeField]
        private string masterDataActorStatusId;
        
        [SerializeField]
        private List<InstanceEquipment> instanceEquipments = new List<InstanceEquipment>();

        [SerializeField]
        private ActorEquipment actorEquipment;
        
        public string Name => this.name;
        
        public IReadOnlyList<InstanceEquipment> InstanceEquipments => this.instanceEquipments;

        public ActorEquipment ActorEquipment => this.actorEquipment;

        public UserData(string masterDataActorStatusId, ActorEquipment actorEquipment)
        {
            this.masterDataActorStatusId = masterDataActorStatusId;
            this.actorEquipment = actorEquipment;
            this.instanceEquipments.AddRange(actorEquipment.InstanceEquipments.Select(x => x.instanceEquipment));
        }

        public void SetName(string name)
        {
            this.name = name;
        }
        
        public void AddInstanceEquipment(InstanceEquipment instanceEquipment)
        {
            this.instanceEquipments.Add(instanceEquipment);
        }
    }
}
