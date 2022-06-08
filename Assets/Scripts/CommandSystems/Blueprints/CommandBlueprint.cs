using System.Collections.Generic;
using TAKACHIYO.CommandSystems.Actions;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Localization;

namespace TAKACHIYO.CommandSystems
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "TAKACHIYO/CommandBlueprint")]
    public sealed class CommandBlueprint : ScriptableObject
    {
        [SerializeField]
        private LocalizedString commandName;
        
        /// <summary>
        /// 詠唱時間（秒）
        /// </summary>
        [SerializeField]
        private float castTime;
        
        [SerializeReference, SubclassSelector(typeof(ICommandAction))]
        private List<ICommandAction> actions;

        public string CommandName => this.commandName.GetLocalizedString();

        public float CastTime => this.castTime;

        public IReadOnlyList<ICommandAction> Actions => this.Actions;
    }
}
