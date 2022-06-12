using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;

namespace TAKACHIYO.MasterDataSystems
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "TAKACHIYO/MasterData/Equipment")]
    public sealed class MasterDataEquipment : MasterData<MasterDataEquipment, MasterDataEquipment.Record>
    {
        [Serializable]
        public class Record : IIdHolder<string>
        {
            public string id;

            public LocalizedString localizedName;

            public int hitPoint;

            public int strength;

            public int defense;

            public int speed;

            public int recoveryPower;

            public string Id => this.id;

            public Record(
                string id,
                string name,
                int hitPoint,
                int strength,
                int defense,
                int speed,
                int recoveryPower
                )
            {
                this.id = id;
                this.localizedName = new LocalizedString();
                this.localizedName.SetReference("Equipment", name);
                this.hitPoint = hitPoint;
                this.strength = strength;
                this.defense = defense;
                this.speed = speed;
                this.recoveryPower = recoveryPower;
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Download")]
        private async void Download()
        {
            var task = DownloadFromSpreadSheet("Equipment");
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

            public string Name;

            public int HitPoint;

            public int Strength;

            public int Defense;

            public int Speed;

            public int RecoveryPower;

            public Record ToRecord() => new Record(
                this.Id,
                this.Name,
                this.HitPoint,
                this.Strength,
                this.Defense,
                this.Speed,
                this.RecoveryPower
                );
        }
#endif
    }
}
