using TAKACHIYO.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.CommandSystems.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Log : CommandAction
    {
        [SerializeField]
        private string message;
        
        public override void Invoke(Actor owner)
        {
            Debug.Log(this.message);
        }
    }
}