using HK.Framework.EventSystems;
using TAKACHIYO.ActorControllers;

namespace TAKACHIYO.BattleSystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BattleEvent
    {
        /// <summary>
        /// バトルのセットアップが完了した際のメッセージ
        /// </summary>
        public class OnSetupBattle : Message<OnSetupBattle, Actor, Actor>
        {
            public Actor Player => this.param1;

            public Actor Enemy => this.param2;
        }

        /// <summary>
        /// バトルが開始された際のメッセージ
        /// </summary>
        public class BattleStart : Message<BattleStart>
        {
        }

        /// <summary>
        /// バトルが終了した際のメッセージ
        /// </summary>
        public class BattleEnd : Message<BattleEnd>
        {
        }
    }
}
