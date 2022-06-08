using HK.Framework.EventSystems;
using TAKACHIYO.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.BattleSystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BattleEvent
    {
        public class OnSetupBattle : Message<OnSetupBattle, Actor, Actor>
        {
            public Actor Player => this.param1;

            public Actor Enemy => this.param2;
        }
    }
}
