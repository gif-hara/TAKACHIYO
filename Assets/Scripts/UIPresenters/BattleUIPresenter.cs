using Cysharp.Threading.Tasks;
using TAKACHIYO.UISystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BattleUIPresenter : UIPresenter
    {
        [SerializeField]
        private ActorStatusUIPresenter playerStatusUIPresenter;

        [SerializeField]
        private ActorStatusUIPresenter enemyStatusUIPresenter;

        public override async UniTask UIInitialize()
        {
            await UniTask.WhenAll(
                this.playerStatusUIPresenter.UIInitialize(),
                this.enemyStatusUIPresenter.UIInitialize()
                );
        }
    }
}
