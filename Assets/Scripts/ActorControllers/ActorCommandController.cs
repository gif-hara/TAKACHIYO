using System;
using System.Collections.Generic;
using System.Linq;
using TAKACHIYO.CommandSystems;
using TAKACHIYO.MasterDataSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.ActorControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ActorCommandController
    {
        private readonly IList<string> commandBlueprintIds;

        private List<CommandBlueprint> commandBlueprints;

        public ActorCommandController(IList<string> commandBlueprintIds)
        {
            this.commandBlueprintIds = commandBlueprintIds;
        }

        public IObservable<Unit> SetupAsync()
        {
            return this.commandBlueprintIds
                .Select(x => AssetLoader.LoadAsync<CommandBlueprint>($"Assets/DataSources/CommandBlueprint/CommandBlueprint.{x}.asset"))
                .WhenAll()
                .Do(x =>
                {
                    this.commandBlueprints = new List<CommandBlueprint>(x);
                })
                .AsUnitObservable();
        }
    }
}
