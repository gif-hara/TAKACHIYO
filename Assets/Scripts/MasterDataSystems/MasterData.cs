using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Networking;

namespace TAKACHIYO.MasterDataSystems
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class MasterData<TMasterData, TRecord> : ScriptableObject
        where TMasterData : MasterData<TMasterData, TRecord>
        where TRecord : IIdHolder<string>
    {
        public static TMasterData Instance { get; private set; }

        [SerializeField]
        protected List<TRecord> records = default;

        public List<TRecord> Records => this.records;

        protected Dictionary<string, TRecord> raw = new();

        private void Setup()
        {
            this.raw.Clear();
            foreach (var i in this.records)
            {
                this.raw.Add(i.Id, i);
            }

            this.OnSetupped();
        }

        protected virtual void OnSetupped()
        {
        }

        public static TRecord Get(string id)
        {
            Assert.IsTrue(Instance.raw.ContainsKey(id), $"{typeof(TMasterData)}に id = {id} が存在しません");
            return Instance.raw[id];
        }

        public static bool Contains(string id)
        {
            return Instance.raw.ContainsKey(id);
        }

        public static IObservable<Unit> SetupAsync(string assetPath)
        {
            return AssetLoader.LoadAsync<TMasterData>(assetPath)
                .Do(x =>
                {
                    Instance = x;
                    Instance.Setup();
                })
                .AsUnitObservable();
        }

        protected async Task<string> DownloadFromSpreadSheet(string sheetName)
        {
            var sheetUrl = File.ReadAllText("masterdata_sheet_url.txt");
            var bearer = File.ReadAllText("bearer.txt");
            var request = UnityWebRequest.Get($"{sheetUrl}?mode={sheetName}");
            request.SetRequestHeader("Authorization", $"Bearer {bearer}");
            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Delay(100);
            }

            var result = operation.webRequest.downloadHandler.text;
            Debug.Log(result);

            return result;
        }
    }

    public abstract class MasterData
    {
        public static IObservable<Unit> SetupAsync()
        {
            return Observable.WhenAll(
                    MasterDataActorStatus.SetupAsync("Assets/DataSources/MasterData/ActorStatus.asset"),
                    MasterDataEquipment.SetupAsync("Assets/DataSources/MasterData/Equipment.asset"),
                    MasterDataEquipmentCommand.SetupAsync("Assets/DataSources/MasterData/EquipmentCommand.asset")
                )
                .AsUnitObservable();
        }
    }
}
