using System;
using HK.Framework;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO
{
    /// <summary>
    /// Pool可能なエフェクト
    /// </summary>
    public sealed class PoolableEffect : MonoBehaviour
    {
        [SerializeField]
        private float delaySeconds;

        private static ObjectPoolBundle<PoolableEffect> poolBundle;

        public PoolableEffect Rent()
        {
            var pool = poolBundle.Get(this);
            var clone = pool.Rent();

            Observable.Timer(TimeSpan.FromSeconds(clone.delaySeconds))
                .TakeUntilDisable(clone)
                .Subscribe(_ =>
                {
                    pool.Return(clone);
                })
                .AddTo(clone);

            return clone;
        }
    }
}
