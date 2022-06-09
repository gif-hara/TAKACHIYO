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
    }
}
