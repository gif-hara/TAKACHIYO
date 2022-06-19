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

        public static async UniTask SetupAsync()
        {
            var prefab = await AssetLoader.LoadAsync<GameObject>("Assets/Prefabs/UI.prefab").ToUniTask();
            Instance = Instantiate(prefab).GetComponent<UIManager>();
            DontDestroyOnLoad(Instance);
        }
    }
}
