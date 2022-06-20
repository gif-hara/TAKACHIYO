using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HK.Framework;
using HK.Framework.EventSystems;
using TAKACHIYO.ActorControllers;
using TAKACHIYO.BootSystems;
using TAKACHIYO.SaveData;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace TAKACHIYO.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EquipmentListUIPresenter : AnimatableUIPresenter
    {
        [SerializeField]
        private GridLayoutGroup listParent;

        [SerializeField]
        private EquipmentButtonUIView equipmentButtonPrefab;

        [SerializeField]
        private Button leftButton;

        [SerializeField]
        private Button rightButton;

        [SerializeField]
        private TextMeshProUGUI page;

        [SerializeField]
        private Button backButton;

        private readonly MessageBroker broker = new();

        private ObjectPool<EquipmentButtonUIView> buttonPool;

        private ReactiveProperty<int> pageNumber = new();

        private ReactiveProperty<int> pageMaxNumber = new();

        private IList<InstanceEquipment> targets;

        private int elementMaxCount;

        public IObservable<InstanceEquipment> SelectInstanceEquipmentAsObservable() => this.broker
            .Receive<SelectInstanceEquipment>()
            .Select(x => x.InstanceEquipment);

        public IObservable<InstanceEquipment> OnMouseEnterInstanceEquipmentAsObservable() => this.broker
            .Receive<OnMouseEnterInstanceEquipment>()
            .Select(x => x.InstanceEquipment);

        public IObservable<Unit> OnClickedBackButtonAsObservable() => this.backButton.OnClickAsObservable();

        public override UniTask UIInitialize()
        {
            this.buttonPool = new ObjectPool<EquipmentButtonUIView>(this.equipmentButtonPrefab);
            var rect = ((RectTransform)this.listParent.transform).rect;
            var columnNumber = Mathf.FloorToInt(rect.width / this.listParent.cellSize.x);
            var rowNumber = Mathf.FloorToInt(rect.height / this.listParent.cellSize.y);
            this.elementMaxCount = columnNumber * rowNumber;

            this.leftButton.OnClickAsObservable()
                .TakeUntil(this.onFinalizeSubject)
                .Subscribe(_ =>
                {
                    if (this.pageNumber.Value <= 0)
                    {
                        this.pageNumber.Value = this.pageMaxNumber.Value;
                    }
                    else
                    {
                        this.pageNumber.Value--;
                    }
                    
                    this.UpdatePage();
                });
            
            this.rightButton.OnClickAsObservable()
                .TakeUntil(this.onFinalizeSubject)
                .Subscribe(_ =>
                {
                    if (this.pageNumber.Value + 1 > this.pageMaxNumber.Value)
                    {
                        this.pageNumber.Value = 0;
                    }
                    else
                    {
                        this.pageNumber.Value++;
                    }
                    
                    this.UpdatePage();
                });

            Observable.Merge(
                    this.pageNumber.AsUnitObservable(),
                    this.pageMaxNumber.AsUnitObservable()
                    )
                .TakeUntil(this.onFinalizeSubject)
                .Subscribe(_ =>
                {
                    this.page.text = $"{this.pageNumber.Value + 1} / {this.pageMaxNumber.Value + 1}";
                });
            
            return base.UIInitialize();
        }

        public override void UIFinalize()
        {
            this.broker.Dispose();
            this.buttonPool.Dispose();
            base.UIFinalize();
        }

        public void Setup(IList<InstanceEquipment> targets, int pageNumber)
        {
            this.targets = targets;
            this.pageNumber.Value = pageNumber;
            this.pageMaxNumber.Value = this.targets.Count / this.elementMaxCount;
            this.UpdatePage();
        }

        private void UpdatePage()
        {
            this.buttonPool.ReturnAll();
            var min = this.pageNumber.Value * this.elementMaxCount;
            for (var i = min; i < min + this.elementMaxCount; i++)
            {
                if (this.targets.Count <= i)
                {
                    return;
                }
                var instanceEquipment = this.targets[i];
                var button = this.buttonPool.Rent();
                button.transform.SetAsLastSibling();
                button.transform.SetParent(this.listParent.transform, false);
                button.SetActiveThumbnail(false);
                instanceEquipment.MasterDataEquipment.GetThumbnail()
                    .ToObservable()
                    .TakeUntil(this.buttonPool.OnBeforeReturnAsObservable(button))
                    .Subscribe(x =>
                    {
                        button.Thumbnail = x;
                        button.SetActiveThumbnail(true);
                    });
                button.OnClickedButtonAsObservable()
                    .TakeUntil(this.buttonPool.OnBeforeReturnAsObservable(button))
                    .Subscribe(_ =>
                    {
                        this.broker.Publish(SelectInstanceEquipment.Get(instanceEquipment));
                    });
                button.OnMouseEnterAsObservable()
                    .TakeUntil(this.buttonPool.OnBeforeReturnAsObservable(button))
                    .Subscribe(_ =>
                    {
                        this.broker.Publish(OnMouseEnterInstanceEquipment.Get(instanceEquipment));
                    });
            }
        }

        /// <summary>
        /// 装備品を選択したメッセージ
        /// </summary>
        public class SelectInstanceEquipment : Message<SelectInstanceEquipment, InstanceEquipment>
        {
            public InstanceEquipment InstanceEquipment => this.param1;
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
