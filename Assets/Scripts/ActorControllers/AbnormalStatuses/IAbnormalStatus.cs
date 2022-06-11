using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.ActorControllers.AbnormalStatuses
{
    /// <summary>
    /// 状態異常のインターフェイス
    /// </summary>
    public interface IAbnormalStatus : IDisposable
    {
        void Setup(Actor owner);
    }
}
