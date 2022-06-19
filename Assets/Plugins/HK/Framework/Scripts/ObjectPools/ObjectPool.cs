using System;
using System.Collections.Generic;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

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

        private MessageBroker broker = new();
        
        public ObjectPool(T original)
        {
            Assert.IsNotNull(original, string.Format("originalがNullでした T = {0}", typeof(T).Name));
            this.original = original;
        }

        protected override void OnBeforeRent(T instance)
        {
            this.instances.Add(instance);
            this.broker.Publish(OnBeforeRentMessage.Get(instance));
            base.OnBeforeRent(instance);
        }

        protected override void OnBeforeReturn(T instance)
        {
            this.instances.Remove(instance);
            this.broker.Publish(OnBeforeReturnMessage.Get(instance));
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

        public IObservable<T> OnBeforeRentAsObservable() => this.broker
            .Receive<OnBeforeRentMessage>()
            .Select(x => x.Instance);

        public IObservable<T> OnBeforeReturnAsObservable() => this.broker
            .Receive<OnBeforeReturnMessage>()
            .Select(x => x.Instance);

        public IObservable<T> OnBeforeReturnAsObservable(T instance) => this.broker
            .Receive<OnBeforeReturnMessage>()
            .Where(x => x.Instance == instance)
            .Select(x => x.Instance);

        protected override void Dispose(bool disposing)
        {
            this.broker.Dispose();
            base.Dispose(disposing);
        }

        public class OnBeforeRentMessage : Message<OnBeforeRentMessage, T>
        {
            public T Instance => this.param1;
        }

        public class OnBeforeReturnMessage : Message<OnBeforeReturnMessage, T>
        {
            public T Instance => this.param1;
        }
    }
}
