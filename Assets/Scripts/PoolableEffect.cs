using System;
using HK.Framework;
using UniRx;
using UnityEngine;

namespace TAKACHIYO
{
    /// <summary>
    /// Pool可能なエフェクト
    /// </summary>
    public sealed class PoolableEffect : MonoBehaviour
    {
        [SerializeField]
        private float delaySeconds;

        private static readonly ObjectPoolBundle<PoolableEffect> poolBundle = new();

        /// <summary>
        /// Poolから実体を返す
        /// </summary>
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
