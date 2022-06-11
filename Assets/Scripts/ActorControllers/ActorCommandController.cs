using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TAKACHIYO.BattleSystems;
using TAKACHIYO.CommandSystems;
using UniRx;
using UnityEngine;

namespace TAKACHIYO.ActorControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ActorCommandController
    {
        private Actor owner;
        
        private readonly IEnumerable<ICommandBlueprintSetupData> commandBlueprintSetupData;
        
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
        
        /// <summary>
        /// コマンドを実行した回数
        /// </summary>
        public int TotalInvokedCount { get; private set; }

        public ActorCommandController(Actor owner, IEnumerable<ICommandBlueprintSetupData> commandBlueprintSetupData)
        {
            this.owner = owner;
            this.commandBlueprintSetupData = commandBlueprintSetupData;
        }

        public void Update(float deltaTime)
        {
            // 睡眠の場合は詠唱出来ない
            if (this.owner.AbnormalStatusController.Contains(Define.AbnormalStatusType.Sleep))
            {
                deltaTime = 0.0f;
            }
            
            // 麻痺の場合は詠唱時間が増える
            if (this.owner.AbnormalStatusController.Contains(Define.AbnormalStatusType.Paralysis))
            {
                deltaTime *= GameDesignParameter.Instance.paralysisDelayRate;
            }

            if (this.owner.AbnormalStatusController.Contains(Define.AbnormalStatusType.FleetSpeed))
            {
                deltaTime *= GameDesignParameter.Instance.fleetSpeedSpeedRate;
            }
            
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
                    this.TotalInvokedCount++;
                    streams.Add(
                        castedCommand.Invoke()
                            .Do(_ =>
                            {
                                this.owner.Broker.Publish(ActorEvent.InvokedCommand.Get());
                            })
                        );
                }

                streams.WhenAll()
                    .Subscribe();
            }
        }

        public async UniTask SetupAsync()
        {
            this.commands = (await UniTask.WhenAll(
                this.commandBlueprintSetupData
                    .Select(async data =>
                    {
                        var result = await AssetLoader.LoadAsync<CommandBlueprint>($"Assets/DataSources/CommandBlueprint/CommandBlueprint.{data.BlueprintId}.asset");
                        return new Command(result, data.BlueprintHolder, this.owner);
                    })
                )).ToList();
        }

        public void AddCastingCommand(Command command)
        {
            command.Reset();
            this.commands.Remove(command);
            this.castingCommands.Add(command);
        }
    }
}
