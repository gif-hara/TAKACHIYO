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
        private Actor owner;
        
        private readonly IList<string> commandBlueprintIds;

        private List<Command> commands;

        private readonly ReactiveCollection<Command> castingCommands = new();

        /// <summary>
        /// 詠唱中のコマンドのリスト
        /// </summary>
        public IReadOnlyReactiveCollection<Command> CastingCommands => this.castingCommands;

        /// <summary>
        /// 詠唱が終わったコマンドのリスト
        /// </summary>
        private readonly List<Command> castedCommands = new List<Command>();

        public ActorCommandController(Actor owner, IList<string> commandBlueprintIds)
        {
            this.owner = owner;
            this.commandBlueprintIds = commandBlueprintIds;

            BattleController.Broker.Receive<BattleEvent.BattleStart>()
                .TakeUntil(BattleController.Broker.Receive<BattleEvent.BattleEnd>())
                .Subscribe(_ =>
                {
                    foreach (var command in this.commands)
                    {
                        this.castingCommands.Add(command);
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
                .Do(commandBlueprints =>
                {
                    this.commands = commandBlueprints.Select(commandBlueprint => new Command(commandBlueprint, this.owner)).ToList();
                })
                .AsUnitObservable();
        }
    }
}
