using System;
using System.Collections.Generic;
using System.Linq;
using HK.Framework.EventSystems;
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
    [Serializable]
    public sealed class ActorEquipment
    {
        [SerializeField]
        private List<InstanceEquipmentData> instanceEquipments = new();

        private readonly MessageBroker broker = new();

        public List<InstanceEquipmentData> InstanceEquipments => this.instanceEquipments;

        public IObservable<UpdateInstanceEquipmentData> UpdateInstanceEquipmentDataAsObservable() => this.broker.Receive<UpdateInstanceEquipmentData>();

        /// <summary>
        /// 総合ヒットポイントを返す
        /// </summary>
        /// <remarks>
        /// 全てのヒットポイントを参照したい場合は<see cref="ActorStatusController.HitPointMax"/>を参照してください
        /// </remarks>
        public int TotalHitPoint => this.InstanceEquipments.Sum(x => x.instanceEquipment.MasterDataEquipment.hitPoint);
        
        /// <summary>
        /// 総合物理攻撃力を返す
        /// </summary>
        public int TotalPhysicsStrength => this.InstanceEquipments.Sum(x => x.instanceEquipment.MasterDataEquipment.physicsStrength);
        
        /// <summary>
        /// 総合物理攻撃力を返す
        /// </summary>
        public int TotalMagicStrength => this.InstanceEquipments.Sum(x => x.instanceEquipment.MasterDataEquipment.magicStrength);
        
        /// <summary>
        /// 総合物理防御力を返す
        /// </summary>
        /// <remarks>
        /// 全ての物理防御力を参照したい場合は<see cref="ActorStatusController.TotalPhysicsDefense"/>を参照してください
        /// </remarks>
        public int TotalPhysicsDefense => this.InstanceEquipments.Sum(x => x.instanceEquipment.MasterDataEquipment.physicsDefense);

        /// <summary>
        /// 総合魔法防御力を返す
        /// </summary>
        /// <remarks>
        /// 全ての魔法防御力を参照したい場合は<see cref="ActorStatusController.TotalMagicDefense"/>を参照してください
        /// </remarks>
        public int TotalMagicDefense => this.InstanceEquipments.Sum(x => x.instanceEquipment.MasterDataEquipment.magicDefense);

        /// <summary>
        /// 総合素早さを返す
        /// </summary>
        /// <remarks>
        /// 全ての素早さを参照したい場合は<see cref="ActorStatusController.TotalSpeed"/>を参照してください
        /// </remarks>
        public int TotalSpeed => this.InstanceEquipments.Sum(x => x.instanceEquipment.MasterDataEquipment.speed);

        public List<ICommandBlueprintSetupData> CreateCommandBlueprintSetupDataList()
        {
            var result = new List<ICommandBlueprintSetupData>();
            foreach (var i in this.InstanceEquipments)
            {
                var masterDataEquipmentCommands = i.instanceEquipment.MasterDataEquipmentCommands;
            
                var commandBlueprintHolder = new DebugCommandBlueprintHolder
                {
                    equipmentPartType = i.equipmentPartType,
                    physicsStrength = Calcurator.GetPhysicsStrength(this, i.instanceEquipment, i.equipmentPartType),
                    magicStrength = Calcurator.GetMagicStrength(this, i.instanceEquipment, i.equipmentPartType),
                    recoveryPower = Calcurator.GetRecoveryPower(this, i.instanceEquipment, i.equipmentPartType)
                };

                result.AddRange(masterDataEquipmentCommands.Select(r => new DebugCommandBlueprintSetupData
                {
                    blueprintId = r.commandBlueprintId,
                    blueprintHolder = commandBlueprintHolder
                }));
            }

            return result;
        }

        /// <summary>
        /// <paramref name="scopeType"/>と一致する装備品を返す
        /// </summary>
        public IEnumerable<InstanceEquipment> Get(Define.EquipmentScopeType scopeType)
        {
            return this.InstanceEquipments
                .Where(x => scopeType.IsMatch(x.equipmentPartType))
                .Select(x => x.instanceEquipment);
        }
        
        /// <summary>
        /// <paramref name="partType"/>の部位に装備している装備品を返す
        /// </summary>
        public InstanceEquipment GetOrNull(Define.EquipmentPartType partType)
        {
            var result = this.InstanceEquipments.Find(x => x.equipmentPartType == partType);
            return result?.instanceEquipment;
        }

        public void AddOrUpdate(Define.EquipmentPartType partType, InstanceEquipment instanceEquipment)
        {
            InstanceEquipmentData updateData;
            var findInstanceEquipment = GetOrNull(partType);
            if (findInstanceEquipment == null)
            {
                updateData = new InstanceEquipmentData
                {
                    equipmentPartType = partType,
                    instanceEquipment = instanceEquipment
                };
                this.instanceEquipments.Add(updateData);
            }
            else
            {
                updateData = this.instanceEquipments.Find(x => x.instanceEquipment == findInstanceEquipment);
                updateData.instanceEquipment = instanceEquipment;
            }
            
            this.broker.Publish(UpdateInstanceEquipmentData.Get(updateData));
        }

        [Serializable]
        public class InstanceEquipmentData
        {
            public Define.EquipmentPartType equipmentPartType;

            public InstanceEquipment instanceEquipment;
        }

        public class UpdateInstanceEquipmentData : Message<UpdateInstanceEquipmentData, InstanceEquipmentData>
        {
            public InstanceEquipmentData InstanceEquipmentData => this.param1;
        }
    }
}
