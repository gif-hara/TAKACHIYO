using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
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

            public int physicsStrength;

            public int physicsDefense;

            public int magicStrength;

            public int magicDefense;

            public int speed;

            public int recoveryPower;

            public Define.EquipmentType equipmentType;

            public Define.AttributeType attributeType1;

            public Define.AttributeType attributeType2;

            public Define.AttributeType attributeType3;

            private List<Define.AttributeType> _attributeTypes = null;

            public List<Define.AttributeType> attributeTypes
            {
                get
                {
// なんかUnityがおかしい. シリアライズされないものでもされているっぽい
#if UNITY_EDITOR
                    this._attributeTypes = null;
#endif
                    if (this._attributeTypes == null)
                    {
                        this._attributeTypes = new List<Define.AttributeType>
                        {
                            this.attributeType1,
                            this.attributeType2,
                            this.attributeType3
                        };
                    }

                    return this._attributeTypes;
                }
            }

            public async UniTask<Sprite> GetThumbnail()
            {
                return await AssetLoader.LoadAsync<Sprite>($"Assets/Textures/Equipment.{this.Id}.png").ToUniTask();
            }

            public string Id => this.id;

            public Record(
                string id,
                string name,
                int hitPoint,
                int physicsStrength,
                int physicsDefense,
                int magicStrength,
                int magicDefense,
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
                this.physicsStrength = physicsStrength;
                this.physicsDefense = physicsDefense;
                this.magicStrength = magicStrength;
                this.magicDefense = magicDefense;
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

            public int PhysicsStrength;

            public int PhysicsDefense;
            
            public int MagicStrength;

            public int MagicDefense;

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
                this.PhysicsStrength,
                this.PhysicsDefense,
                this.MagicStrength,
                this.MagicDefense,
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
