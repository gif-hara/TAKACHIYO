using TAKACHIYO.MasterDataSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.ActorControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Actor
    {
        public ActorStatusController StatusController { get; }
        
        public Actor(MasterDataActorStatus.Record masterDataActorStatus)
        {
            this.StatusController = new ActorStatusController(masterDataActorStatus);
        }
    }
}
