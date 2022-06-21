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

        public override void UIFinalize()
        {
            this.playerStatusUIPresenter.UIFinalize();
            this.enemyStatusUIPresenter.UIFinalize();
            base.UIFinalize();
        }

        public override async UniTask OpenAsync()
        {
            await UniTask.WhenAll(
                base.OpenAsync(),
                this.playerStatusUIPresenter.OpenAsync(),
                this.enemyStatusUIPresenter.OpenAsync()
                );
        }

        public override async UniTask CloseAsync()
        {
            await UniTask.WhenAll(
                this.playerStatusUIPresenter.CloseAsync(),
                this.enemyStatusUIPresenter.CloseAsync()
                );
        }
    }
}
