using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO
{
    /// <summary>
    /// ゲームデザインに必要なパラメーターを置く場所
    /// </summary>
    [CreateAssetMenu(menuName = "TAKACHIYO/GameDesignParameter")]
    public sealed class GameDesignParameter : ScriptableObject
    {
        public static GameDesignParameter Instance { get; private set; }

        [SerializeField, Range(0.0f, 1.0f)]
        private float poisonDamageRate;

        [SerializeField]
        private float poisonDamageSeconds;

        [SerializeField]
        private int poisonDamageCount;
        
        /// <summary>
        /// 毒ダメージの割合
        /// </summary>
        /// <remarks>
        /// <c>0.25</c>にした場合1/4の割合ダメージを与えます
        /// </remarks>
        public float PoisonDamageRate => this.poisonDamageRate;
        
        /// <summary>
        /// 毒ダメージを与える秒数
        /// </summary>
        public float PoisonDamageSeconds => this.poisonDamageSeconds;

        /// <summary>
        /// 毒ダメージを与える回数
        /// </summary>
        public int PoisonDamageCount => this.poisonDamageCount;

        public static async UniTask LoadAsync()
        {
            Instance = await AssetLoader.LoadAsync<GameDesignParameter>("Assets/DataSources/GameDesignParameter.asset");
        }
    }
}
