using System;
using HK.Framework;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace TAKACHIYO
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EditEquipmentButtonUIView : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private Image thumbnail;
        
        private ObjectPool<EditEquipmentButtonUIView> pool;

        private static ObjectPoolBundle<EditEquipmentButtonUIView> objectPoolBundle = new();

        public IObservable<Unit> OnClickedButtonAsObservable() => this.button.OnClickAsObservable();

        public Sprite Thumbnail
        {
            set => this.thumbnail.sprite = value;
        }

        public EditEquipmentButtonUIView Rent()
        {
            if (this.pool == null)
            {
                this.pool = objectPoolBundle.Get(this);
            }

            return this.pool.Rent();
        }

        public void Return()
        {
            Assert.IsNotNull(this.pool);
            this.pool.Return(this);
        }
    }
}
