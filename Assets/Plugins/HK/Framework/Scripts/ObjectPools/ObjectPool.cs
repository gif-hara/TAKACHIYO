using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Framework
{
    /// <summary>
    /// オブジェクトプール
    /// </summary>
    public class ObjectPool<T> : UniRx.Toolkit.ObjectPool<T>
        where T : Component
    {
        private readonly T original;

        private List<T> instances = new();
        
        public ObjectPool(T original)
        {
            Assert.IsNotNull(original, string.Format("originalがNullでした T = {0}", typeof(T).Name));
            this.original = original;
        }

        protected override void OnBeforeRent(T instance)
        {
            this.instances.Add(instance);
            base.OnBeforeRent(instance);
        }

        protected override void OnBeforeReturn(T instance)
        {
            this.instances.Remove(instance);
            base.OnBeforeReturn(instance);
        }

        protected override T CreateInstance()
        {
            return Object.Instantiate(this.original);
        }

        public void ReturnAll()
        {
            var tempList = new List<T>(this.instances);
            foreach (var i in tempList)
            {
                this.Return(i);
            }
            
            this.instances.Clear();
        }
    }
}
