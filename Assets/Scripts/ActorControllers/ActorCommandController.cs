using System;
using System.Collections.Generic;
using System.Linq;
using TAKACHIYO.BattleSystems;
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

        private readonly ReactiveCollection<Command> castingCommands = new();

        /// <summary>
        /// 詠唱中のコマンドのリスト
        /// </summary>
        public IReadOnlyCollection<Command> CastingCommands => this.castingCommands;

        /// <summary>
        /// 詠唱が終わったコマンドのリスト
        /// </summary>
        private readonly List<Command> castedCommands = new List<Command>();

        public ActorCommandController(IList<string> commandBlueprintIds)
        {
            this.commandBlueprintIds = commandBlueprintIds;

            BattleController.Broker.Receive<BattleEvent.BattleStart>()
                .TakeUntil(BattleController.Broker.Receive<BattleEvent.BattleEnd>())
                .Subscribe(_ =>
                {
                    foreach (var commandBlueprint in this.commandBlueprints)
                    {
                        this.castingCommands.Add(new Command(commandBlueprint));
                    }
                });
        }

        public void Update(float deltaTime)
        {
            this.castedCommands.Clear();
            foreach (var castingCommand in this.castingCommands)
            {
                castingCommand.Update(deltaTime);
                if (castingCommand.CanInvoke)
                {
                    this.castedCommands.Add(castingCommand);
                }
            }
            foreach (var castedCommand in this.castedCommands)
            {
                castedCommand.Invoke();
                this.castingCommands.Remove(castedCommand);
            }
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
