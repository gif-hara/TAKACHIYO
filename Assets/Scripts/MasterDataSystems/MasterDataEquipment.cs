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

            public Define.EquipmentType equipmentType;

            public Define.AttributeType attributeType1;

            public Define.AttributeType attributeType2;

            public Define.AttributeType attributeType3;

            private List<Define.AttributeType> _attributeTypes;

            public List<Define.AttributeType> attributeTypes => this._attributeTypes ??= new List<Define.AttributeType>
            {
                this.attributeType1,
                this.attributeType2,
                this.attributeType3,
            };

            public string Id => this.id;

            public Record(
                string id,
                string name,
                int hitPoint,
                int strength,
                int defense,
                int speed,
                int recoveryPower,
                Define.EquipmentType equipmentType,
                Define.AttributeType attributeType1,
                Define.AttributeType attributeType2,
                Define.AttributeType attributeType3
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
                this.equipmentType = equipmentType;
                this.attributeType1 = attributeType1;
                this.attributeType2 = attributeType2;
                this.attributeType3 = attributeType3;
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

            public string EquipmentType;

            public string AttributeType1;
            
            public string AttributeType2;
            
            public string AttributeType3;

            public Record ToRecord() => new Record(
                this.Id,
                this.Name,
                this.HitPoint,
                this.Strength,
                this.Defense,
                this.Speed,
                this.RecoveryPower,
                Define.ConvertToEquipmentType(this.EquipmentType),
                Define.ConvertToAttributeType(this.AttributeType1),
                Define.ConvertToAttributeType(this.AttributeType2),
                Define.ConvertToAttributeType(this.AttributeType3)
                );
        }
#endif
    }
}
