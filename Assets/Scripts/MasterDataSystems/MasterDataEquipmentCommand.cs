using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Localization;

namespace TAKACHIYO.MasterDataSystems
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "TAKACHIYO/MasterData/EquipmentCommand")]
    public sealed class MasterDataEquipmentCommand : MasterData<MasterDataEquipmentCommand, MasterDataEquipmentCommand.Record>
    {
        private Dictionary<string, List<Record>> equipmentIdTable = new ();
        
        [Serializable]
        public class Record : IIdHolder<string>
        {
            public string id;

            public string equipmentId;

            public string commandBlueprintId;
            
            public string Id => this.id;

            public Record(
                string id,
                string equipmentId,
                string commandBlueprintId
                )
            {
                this.id = id;
                this.equipmentId = equipmentId;
                this.commandBlueprintId = commandBlueprintId;
            }
        }

        protected override void OnSetupped()
        {
            base.OnSetupped();
            
            foreach (var r in this.records)
            {
                if (!this.equipmentIdTable.ContainsKey(r.equipmentId))
                {
                    this.equipmentIdTable.Add(r.equipmentId, new List<Record>());
                }
                
                this.equipmentIdTable[r.equipmentId].Add(r);
            }
        }

        public static IReadOnlyList<Record> GetFromEquipmentId(string equipmentId)
        {
            Assert.IsTrue(Instance.equipmentIdTable.ContainsKey(equipmentId), $"{nameof(equipmentId)} = {equipmentId}は存在しません");
            return Instance.equipmentIdTable[equipmentId];
        }

#if UNITY_EDITOR
        [ContextMenu("Download")]
        private async void Download()
        {
            var task = DownloadFromSpreadSheet("EquipmentCommand");
            await task;

            var result = JsonUtility.FromJson<Json>(task.Result);

            this.records = result.elements.Select(x => x.ToRecord()).ToList();
            UnityEditor.EditorUtility.SetDirty(this);
        }

        [Serializable]
        private class Json
        {
            public List<JsonElement> elements;
        }

        [Serializable]
        private class JsonElement
        {
            public string Id;

            public string EquipmentId;

            public string CommandBlueprintId;

            public Record ToRecord() => new Record(
                this.Id,
                this.EquipmentId,
                this.CommandBlueprintId
                );
        }
#endif
    }
}
