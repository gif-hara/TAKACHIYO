using System.Collections.Generic;
using TAKACHIYO.CommandSystems.Actions;
using UnityEngine;
using UnityEngine.Localization;

namespace TAKACHIYO.CommandSystems
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "TAKACHIYO/CommandActionBundle")]
    public sealed class CommandActionBundle : ScriptableObject
    {
        [SerializeField]
        private LocalizedString commandName;

        [SerializeField]
        private float castTime;
        
        [SerializeReference, SubclassSelector(typeof(ICommandAction))]
        private List<ICommandAction> actions;

        public string CommandName => this.commandName.GetLocalizedString();

        public float CastTime => this.castTime;

        public IReadOnlyList<ICommandAction> Actions => this.actions;
    }
}
