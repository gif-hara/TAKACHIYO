using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HK.Framework;
using TAKACHIYO.ActorControllers;
using TAKACHIYO.BootSystems;
using TAKACHIYO.SaveData;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;
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

        private readonly Subject<InstanceEquipment> selectInstanceEquipmentSubject = new();

        private ObjectPool<EquipmentButtonUIView> buttonPool;

        private int pageNumber = 0;

        private IList<InstanceEquipment> targets;

        private int elementMaxCount;

        public IObservable<InstanceEquipment> SelectInstanceEquipmentAsObservable() => this.selectInstanceEquipmentSubject;

        private void Awake()
        {
            this.buttonPool = new ObjectPool<EquipmentButtonUIView>(this.equipmentButtonPrefab);
            var rect = ((RectTransform)this.listParent.transform).rect;
            var columnNumber = Mathf.FloorToInt(rect.width / this.listParent.cellSize.x);
            var rowNumber = Mathf.FloorToInt(rect.height / this.listParent.cellSize.y);
            this.elementMaxCount = columnNumber * rowNumber;
        }
        
        public override void UIFinalize()
        {
            this.selectInstanceEquipmentSubject.Dispose();
            this.buttonPool.Dispose();
            base.UIFinalize();
        }

        public void Setup(IList<InstanceEquipment> targets, int pageNumber)
        {
            this.targets = targets;
            this.UpdatePage(pageNumber);
        }

        private void UpdatePage(int pageNumber)
        {
            this.pageNumber = pageNumber;
            this.buttonPool.ReturnAll();
            var min = this.pageNumber * this.elementMaxCount;
            for (var i = min; i < min + this.elementMaxCount; i++)
            {
                if (this.targets.Count <= i)
                {
                    return;
                }
                var instanceEquipment = this.targets[i];
                var button = this.buttonPool.Rent();
                button.transform.SetParent(this.listParent.transform, false);
                button.SetActiveThumbnail(false);
                instanceEquipment.MasterDataEquipment.GetThumbnail()
                    .ToObservable()
                    .Subscribe(x =>
                    {
                        button.Thumbnail = x;
                        button.SetActiveThumbnail(true);
                    });
                button.OnClickedButtonAsObservable()
                    .TakeUntil(this.buttonPool.OnBeforeReturnAsObservable(button))
                    .Subscribe(_ =>
                    {
                        this.selectInstanceEquipmentSubject.OnNext(instanceEquipment);
                    });
            }
        }
    }
}
