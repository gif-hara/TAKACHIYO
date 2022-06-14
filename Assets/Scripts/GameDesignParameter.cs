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

        /// <summary>
        /// 防御力の基本値
        /// </summary>
        public int baseDefense;

        /// <summary>
        /// 素早さの基本値
        /// </summary>
        public int baseSpeed;

        /// <summary>
        /// 毒ダメージの割合
        /// </summary>
        /// <remarks>
        /// <c>0.25</c>にした場合1/4の割合ダメージを与えます
        /// </remarks>
        [Range(0.0f, 1.0f)]
        public float poisonDamageRate;

        /// <summary>
        /// 毒ダメージを与える秒数
        /// </summary>
        public float poisonDamageSeconds;

        /// <summary>
        /// 毒ダメージを与える回数
        /// </summary>
        public int poisonDamageCount;

        /// <summary>
        /// 麻痺にかかっている時間（秒）
        /// </summary>
        public float paralysisTimeSeconds;

        /// <summary>
        /// 麻痺による詠唱時間減少の割合
        /// </summary>
        [Range(0.0f, 1.0f)]
        public float paralysisDelayRate;

        /// <summary>
        /// 睡眠にかかっている時間（秒）
        /// </summary>
        public float sleepTimeSeconds;

        /// <summary>
        /// 脱力にかかっている時間（秒）
        /// </summary>
        public float exhaustionTimeSeconds;

        /// <summary>
        /// 脱力によるダメージ減少の割合
        /// </summary>
        [Range(0.0f, 1.0f)]
        public float exhaustionDamageRate;

        /// <summary>
        /// 脆弱にかかっている時間（秒）
        /// </summary>
        public float brittleTimeSeconds;

        /// <summary>
        /// 脆弱によるダメージ増加の割合
        /// </summary>
        public float brittleDamageRate;

        /// <summary>
        /// 躓きにかかっている時間（秒）
        /// </summary>
        public float tripTimeSeconds;
        
        /// <summary>
        /// 癒しダメージの割合
        /// </summary>
        /// <remarks>
        /// <c>0.25</c>にした場合1/4の割合ダメージを与えます
        /// </remarks>
        [Range(0.0f, 1.0f)]
        public float healingDamageRate;

        /// <summary>
        /// 癒しダメージを与える秒数
        /// </summary>
        public float healingDamageSeconds;

        /// <summary>
        /// 癒しダメージを与える回数
        /// </summary>
        public int healingDamageCount;
        
        /// <summary>
        /// 俊足にかかっている時間（秒）
        /// </summary>
        public float fleetSpeedTimeSeconds;

        /// <summary>
        /// 俊足による詠唱時間増加の割合
        /// </summary>
        public float fleetSpeedSpeedRate;
        
        /// <summary>
        /// 怪力にかかっている時間（秒）
        /// </summary>
        public float strongTimeSeconds;

        /// <summary>
        /// 怪力によるダメージ増加の割合
        /// </summary>
        public float strongDamageRate;
        
        /// <summary>
        /// 頑強にかかっている時間（秒）
        /// </summary>
        public float stubbornTimeSeconds;

        /// <summary>
        /// 頑強によるダメージ減少の割合
        /// </summary>
        [Range(0.0f, 1.0f)]
        public float stubbornDamageRate;

        /// <summary>
        /// 体力増強にかかっている時間（秒）
        /// </summary>
        public float physicalStrengthTimeSeconds;

        /// <summary>
        /// 体力増強によるHP増加の割合
        /// </summary>
        public float physicalStrengthHitPointUpRate;

        /// <summary>
        /// 鉄壁にかかっている時間（秒）
        /// </summary>
        public float ironWallTimeSeconds;

        /// <summary>
        /// 跳返にかかっている時間（秒）
        /// </summary>
        public float counterTimeSeconds;

        /// <summary>
        /// 跳返による返すダメージの割合
        /// </summary>
        public float counterDamageRate;

        /// <summary>
        /// 吸収にかかっている時間（秒）
        /// </summary>
        public float absorptionTimeSeconds;

        /// <summary>
        /// 吸収による回復量の割合
        /// </summary>
        public float absorptionRecoveryRate;
        
        public static async UniTask LoadAsync()
        {
            Instance = await AssetLoader.LoadAsync<GameDesignParameter>("Assets/DataSources/GameDesignParameter.asset");
        }
    }
}
