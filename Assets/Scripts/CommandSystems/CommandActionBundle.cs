using System;
using System.Collections.Generic;
using TAKACHIYO.CommandSystems.Actions;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Search;

namespace TAKACHIYO.CommandSystems
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class CommandActionBundle
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
