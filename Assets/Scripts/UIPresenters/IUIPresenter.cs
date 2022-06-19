using Cysharp.Threading.Tasks;

namespace TAKACHIYO.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUIPresenter
    {
        /// <summary>
        /// UIの初期化を行う
        /// </summary>
        UniTask UIInitialize();

        /// <summary>
        /// UIを開く
        /// </summary>
        UniTask OpenAsync();

        /// <summary>
        /// UIを閉じる
        /// </summary>
        UniTask CloseAsync();

        /// <summary>
        /// UIの終了処理を行う
        /// </summary>
        void UIFinalize();
    }
}
