using System;
using System.Collections.Generic;
using System.Linq;
using TAKACHIYO.BattleSystems;
using TAKACHIYO.CommandSystems;
using UniRx;

namespace TAKACHIYO.ActorControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ActorCommandController
    {
        private Actor owner;
        
        private readonly IList<string> commandBlueprintIds;
        
        /// <summary>
        /// 待機中のコマンドのリスト
        /// </summary>
        private List<Command> commands;

        private readonly ReactiveCollection<Command> castingCommands = new();

        /// <summary>
        /// 詠唱中のコマンドのリスト
        /// </summary>
        public IReadOnlyReactiveCollection<Command> CastingCommands => this.castingCommands;

        /// <summary>
        /// 詠唱を開始したコマンドのリスト
        /// </summary>
        private readonly List<Command> enterCastingCommands = new();
        
        /// <summary>
        /// 詠唱が終わったコマンドのリスト
        /// </summary>
        private readonly List<Command> castedCommands = new();

        public ActorCommandController(Actor owner, IList<string> commandBlueprintIds)
        {
            this.owner = owner;
            this.commandBlueprintIds = commandBlueprintIds;

            BattleController.Broker.Receive<BattleEvent.BattleStart>()
                .TakeUntil(BattleController.Broker.Receive<BattleEvent.BattleEnd>())
                .Subscribe(_ =>
                {
                    this.TryCastingCommands();
                });

            this.owner.Broker.Receive<ActorEvent.InvokedCommand>()
                .TakeUntil(BattleController.Broker.Receive<BattleEvent.BattleEnd>())
                .Subscribe(_ =>
                {
                    this.TryCastingCommands();
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

            if (this.castedCommands.Count > 0)
            {
                var streams = new List<IObservable<Unit>>();
                foreach (var castedCommand in this.castedCommands)
                {
                    this.commands.Add(castedCommand);
                    this.castingCommands.Remove(castedCommand);
                    streams.Add(castedCommand.Invoke());
                }

                streams.Concat()
                    .AsSingleUnitObservable()
                    .Subscribe(_ =>
                    {
                        this.owner.Broker.Publish(ActorEvent.InvokedCommand.Get());
                    });
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
        
        /// <summary>
        /// 待機中のコマンドが詠唱できる場合は詠唱を開始する
        /// </summary>
        private void TryCastingCommands()
        {
            this.enterCastingCommands.Clear();
            foreach (var command in this.commands)
            {
                if (command.CanCasting)
                {
                    this.enterCastingCommands.Add(command);
                }
            }

            foreach (var command in this.enterCastingCommands)
            {
                command.Reset();
                this.commands.Remove(command);
                this.castingCommands.Add(command);
            }
        }
    }
}
