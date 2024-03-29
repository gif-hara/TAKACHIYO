using System;
using Cysharp.Threading.Tasks;
using HK.Framework;
using HK.Framework.EventSystems;
using TAKACHIYO.ActorControllers;
using TAKACHIYO.BootSystems;
using TAKACHIYO.SaveData;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;

namespace TAKACHIYO.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EquipmentChangeUIPresenter : AnimatableUIPresenter
    {
        [SerializeField]
        private Transform currentEquipmentParent;

        [SerializeField]
        private EquipmentButtonUIView equipmentButtonPrefab;

        private readonly MessageBroker broker = new();

        private ObjectPool<EquipmentButtonUIView> buttonPool;

        public IObservable<OnRequestOpenList> OnRequestOpenListAsObservable() => this.broker.Receive<OnRequestOpenList>();

        public IObservable<InstanceEquipment> OnMouseEnterInstanceEquipmentAsObservable() => this.broker
            .Receive<OnMouseEnterInstanceEquipment>()
            .Select(x => x.InstanceEquipment);

        public override async UniTask UIInitialize()
        {
            this.buttonPool = new ObjectPool<EquipmentButtonUIView>(this.equipmentButtonPrefab);
            this.buttonPool.ReturnAll();
            await UniTask.WhenAll(
                this.CreateButton(Define.EquipmentPartType.MainWeapon),
                this.CreateButton(Define.EquipmentPartType.ArmorHead),
                this.CreateButton(Define.EquipmentPartType.ArmorChest),
                this.CreateButton(Define.EquipmentPartType.SubWeapon1),
                this.CreateButton(Define.EquipmentPartType.ArmorArms),
                this.CreateButton(Define.EquipmentPartType.ArmorTorso),
                this.CreateButton(Define.EquipmentPartType.SubWeapon2),
                this.CreateButton(Define.EquipmentPartType.ArmorLegs),
                this.CreateButton(Define.EquipmentPartType.Accessory)
                );

            await base.UIInitialize();
        }
        
        public override void UIFinalize()
        {
            this.broker.Dispose();
            this.buttonPool.Dispose();
            base.UIFinalize();
        }

        private async UniTask CreateButton(Define.EquipmentPartType partType)
        {
            var userData = UserData.Instance;
            var button = this.buttonPool.Rent();
            button.transform.SetParent(this.currentEquipmentParent, false);
            button.Thumbnail = null;
            button.Message = partType.LocalizedString();
            button.SetActiveThumbnail(false);

            var instanceEquipment = userData.ActorEquipment.GetOrNull(partType);
            if (instanceEquipment != null)
            {
                button.Thumbnail = await instanceEquipment.MasterDataEquipment.GetThumbnail();
                button.SetActiveThumbnail(true);
            }

            button.OnMouseEnterAsObservable()
                .TakeUntil(this.buttonPool.OnBeforeReturnAsObservable(button))
                .Subscribe(_ =>
                {
                    this.broker.Publish(OnMouseEnterInstanceEquipment.Get(userData.ActorEquipment.GetOrNull(partType)));
                });

            button.OnClickedButtonAsObservable()
                .TakeUntil(this.buttonPool.OnBeforeReturnAsObservable(button))
                .Subscribe(x =>
                {
                    this.broker.Publish(OnRequestOpenList.Get(partType));
                });

            userData.ActorEquipment.UpdateInstanceEquipmentDataAsObservable()
                .TakeUntil(this.buttonPool.OnBeforeReturnAsObservable(button))
                .Where(x => x.InstanceEquipmentData.equipmentPartType == partType)
                .Subscribe(async x =>
                {
                    if (x.InstanceEquipmentData.instanceEquipment != null)
                    {
                        button.Thumbnail = await x.InstanceEquipmentData.instanceEquipment.MasterDataEquipment.GetThumbnail();
                        button.SetActiveThumbnail(true);
                    }
                });
        }

        /// <summary>
        /// 装備変更するためにリストの表示をリクエストするメッセージ
        /// </summary>
        public class OnRequestOpenList : Message<OnRequestOpenList, Define.EquipmentPartType>
        {
            /// <summary>
            /// 変更したい部位
            /// </summary>
            public Define.EquipmentPartType ChangeTarget => this.param1;
        }

        /// <summary>
        /// マウスがホバーした装備品を発行するメッセージ
        /// </summary>
        public class OnMouseEnterInstanceEquipment : Message<OnMouseEnterInstanceEquipment, InstanceEquipment>
        {
            public InstanceEquipment InstanceEquipment => this.param1;
        }
    }
}
