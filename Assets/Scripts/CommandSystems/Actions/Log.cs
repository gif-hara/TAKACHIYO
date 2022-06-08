using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.CommandSystems.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Log : ICommandAction
    {
        [SerializeField]
        private string message;
        
        public void Invoke()
        {
            Debug.Log(this.message);
        }
    }
}
