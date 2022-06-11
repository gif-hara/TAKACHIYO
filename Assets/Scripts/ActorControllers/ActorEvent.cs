using HK.Framework.EventSystems;

namespace TAKACHIYO.ActorControllers
{
    /// <summary>
    /// 
    /// </summary>
    public static class ActorEvent
    {
        /// <summary>
        /// コマンドを実行した際のメッセージ
        /// </summary>
        public class InvokedCommand : Message<InvokedCommand>
        {
        }

        /// <summary>
        /// ダメージを受けた際のメッセージ
        /// </summary>
        public class TakedDamage : Message<TakedDamage, int>
        {
            public int Damage => this.param1;
        }

        /// <summary>
        /// ダメージを与えた際のメッセージ
        /// </summary>
        public class GivedDamage : Message<GivedDamage>
        {
        }

        /// <summary>
        /// 状態異常が付与された際のメッセージ
        /// </summary>
        public class AddedAbnormalStatus : Message<AddedAbnormalStatus, Define.AbnormalStatusType>
        {
            public Define.AbnormalStatusType AbnormalStatusType => this.param1;
        }

        /// <summary>
        /// 状態異常が削除された際のメッセージ
        /// </summary>
        public class RemovedAbnormalStatus : Message<RemovedAbnormalStatus, Define.AbnormalStatusType>
        {
            public Define.AbnormalStatusType AbnormalStatusType => this.param1;
        }

        /// <summary>
        /// エフェクトの生成をリクエストするメッセージ
        /// </summary>
        public class RequestInstantiateEffect : Message<RequestInstantiateEffect, PoolableEffect>
        {
            public PoolableEffect EffectPrefab => this.param1;
        }
    }
}
