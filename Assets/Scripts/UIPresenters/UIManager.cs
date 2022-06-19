using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        
        [SerializeField]
        private Transform root;

        private List<UIPresenter> presenters = new();

        private UIPresenter currentPresenter;

        public static async UniTask SetupAsync()
        {
            var prefab = await AssetLoader.LoadAsync<GameObject>("Assets/Prefabs/UI.prefab").ToUniTask();
            Instance = Instantiate(prefab).GetComponent<UIManager>();
            DontDestroyOnLoad(Instance);
        }

        public async UniTask<UIPresenter> OpenAsync(UIPresenter presenterPrefab)
        {
            var newPresenter = Instantiate(presenterPrefab, this.root);
            newPresenter.gameObject.SetActive(false);
            await newPresenter.UIInitialize();
            if (this.currentPresenter != null)
            {
                this.presenters.Add(this.currentPresenter);
                var oldPresenter = this.currentPresenter;
                this.currentPresenter = newPresenter;
                await UniTask.WhenAll(
                    newPresenter.OpenAsync(),
                    oldPresenter.CloseAsync()
                    );
            }
            else
            {
                this.currentPresenter = newPresenter;
                await this.currentPresenter.OpenAsync();
            }

            return newPresenter;
        }

        public async UniTask CloseAsync()
        {
            if (this.currentPresenter == null)
            {
                return;
            }

            if (this.presenters.Count > 0)
            {
                var oldPresenter = this.currentPresenter;
                var lastIndex = this.presenters.Count - 1;
                this.currentPresenter = this.presenters[lastIndex];
                this.presenters.RemoveAt(lastIndex);

                await UniTask.WhenAll(
                    this.currentPresenter.OpenAsync(),
                    oldPresenter.CloseAsync()
                    );
                
                oldPresenter.UIFinalize();
            }
            else
            {
                var oldPresenter = this.currentPresenter;
                this.currentPresenter = null;
                await oldPresenter.CloseAsync();
            }
        }
    }
}
