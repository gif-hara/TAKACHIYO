using UnityEngine.Assertions;

namespace TAKACHIYO
{
    /// <summary>
    /// 定数定義
    /// </summary>
    public static class Define
    {
        /// <summary>
        /// アクターのタイプ
        /// </summary>
        public enum ActorType
        {
            Player,
            Enemy,
        }

        /// <summary>
        /// ターゲットのタイプ
        /// </summary>
        public enum TargetType
        {
            /// <summary>
            /// 自分
            /// </summary>
            My,
            
            /// <summary>
            /// 相手
            /// </summary>
            Opponent,
            
            /// <summary>
            /// 全員
            /// </summary>
            All,
        }

        /// <summary>
        /// バトルの勝敗結果
        /// </summary>
        public enum BattleJudgeType
        {
            PlayerWin,
            EnemyWin,
            Draw,
        }

        /// <summary>
        /// 比較タイプ
        /// </summary>
        public enum CompareType
        {
            /// <summary>
            /// 以上
            /// </summary>
            Greater,
            
            /// <summary>
            /// 以下
            /// </summary>
            Less
        }

        /// <summary>
        /// 状態異常のタイプ
        /// </summary>
        public enum AbnormalStatusType
        {
            /// <summary>
            /// 毒
            /// 一定時間ダメージを受ける
            /// </summary>
            Poison,
            
            /// <summary>
            /// 麻痺
            /// 一定時間詠唱時間が倍になる
            /// </summary>
            Paralysis,
            
            /// <summary>
            /// 睡眠
            /// 一定時間詠唱ができない
            /// </summary>
            Sleep,
            
            /// <summary>
            /// 脱力
            /// 一定時間与えるダメージが減る
            /// </summary>
            Exhaustion,
            
            /// <summary>
            /// 脆弱
            /// 一定時間受けるダメージが増える
            /// </summary>
            Brittle,
            
            /// <summary>
            /// 躓き
            /// 詠唱時間がリセットされる
            /// </summary>
            Trip,
            
            /// <summary>
            /// 癒し
            /// 一定時間回復する
            /// </summary>
            Healing,
            
            /// <summary>
            /// 俊足
            /// 一定時間詠唱時間が早くなる
            /// </summary>
            FleetSpeed,
            
            /// <summary>
            /// 怪力
            /// 一定時間与えるダメージが増える
            /// </summary>
            Strong,
            
            /// <summary>
            /// 頑強
            /// 一定時間受けるダメージが減る
            /// </summary>
            Stubborn,
            
            /// <summary>
            /// 体力増強
            /// 一定時間HPが増える
            /// </summary>
            PhysicalStrength,
            
            /// <summary>
            /// 鉄壁
            /// 一度だけダメージを無効化する. その後この状態異常は解除される
            /// </summary>
            IronWall,
        }

        /// <summary>
        /// 属性タイプ
        /// </summary>
        public enum AttributeType
        {
            /// <summary>なし</summary>
            None,
            /// <summary>長剣</summary>
            LongSword,
            /// <summary>短剣</summary>
            Dagger,
            /// <summary>盾</summary>
            Shield,
            /// <summary>杖</summary>
            Rod,
            /// <summary>弓</summary>
            Bow,
            /// <summary>革</summary>
            Leather,
            /// <summary>金属</summary>
            Metal,
            /// <summary>木材</summary>
            Wood,
            /// <summary>火</summary>
            Fire,
            /// <summary>氷</summary>
            Ice,
            /// <summary>雷</summary>
            Thunder,
            /// <summary>土</summary>
            Dirt,
            /// <summary>光</summary>
            Holy,
            /// <summary>闇</summary>
            Dark,
            /// <summary>地上</summary>
            Surface,
            /// <summary>飛行</summary>
            Fly,
            /// <summary>人間</summary>
            Human,
            /// <summary>獣</summary>
            Beast
        }

        /// <summary>
        /// 装備品タイプ
        /// </summary>
        public enum EquipmentType
        {
            Weapon,
            ArmorHead,
            ArmorChest,
            ArmorArms,
            ArmorTorso,
            ArmorLegs,
            Accessory,
        }
        
        public enum EquipmentPartType
        {
            MainWeapon,
            SubWeapon1,
            SubWeapon2,
            ArmorHead,
            ArmorChest,
            ArmorArms,
            ArmorTorso,
            ArmorLegs,
            Accessory,
        }

        /// <summary>
        /// 攻撃タイプ
        /// </summary>
        public enum AttackType
        {
            /// <summary>
            /// 物理
            /// </summary>
            Physics,
            
            /// <summary>
            /// 魔法
            /// </summary>
            Magic,
        }

        public static EquipmentType ConvertToEquipmentType(string value)
        {
            switch (value)
            {
                case "武器":
                    return EquipmentType.Weapon;
                case "頭":
                    return EquipmentType.ArmorHead;
                case "胴":
                    return EquipmentType.ArmorChest;
                case "腕":
                    return EquipmentType.ArmorArms;
                case "腰":
                    return EquipmentType.ArmorTorso;
                case "脚":
                    return EquipmentType.ArmorLegs;
                case "アクセサリー":
                    return EquipmentType.Accessory;
                default:
                    Assert.IsTrue(false, $"{value}は未対応です");
                    return default;
            }
        }

        public static AttributeType ConvertToAttributeType(string value)
        {
            switch (value)
            {
                case "なし":
                    return AttributeType.None;
                case "長剣":
                    return AttributeType.LongSword;
                case "短剣":
                    return AttributeType.Dagger;
                case "盾":
                    return AttributeType.Shield;
                case "杖":
                    return AttributeType.Rod;
                case "弓":
                    return AttributeType.Bow;
                case "革":
                    return AttributeType.Leather;
                case "金属":
                    return AttributeType.Metal;
                case "木材":
                    return AttributeType.Wood;
                case "火":
                    return AttributeType.Fire;
                case "氷":
                    return AttributeType.Ice;
                case "雷":
                    return AttributeType.Thunder;
                case "土":
                    return AttributeType.Dirt;
                case "光":
                    return AttributeType.Holy;
                case "闇":
                    return AttributeType.Dark;
                case "地上":
                    return AttributeType.Surface;
                case "飛行":
                    return AttributeType.Fly;
                case "人間":
                    return AttributeType.Human;
                case "獣":
                    return AttributeType.Beast;
                default:
                    Assert.IsTrue(false, $"{value}は未対応です");
                    return default;
            }
        }
    }
}
