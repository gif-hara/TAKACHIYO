using System.Collections.Generic;
using TAKACHIYO.ActorControllers;
using TAKACHIYO.BattleSystems;
using TAKACHIYO.CommandSystems;
using TAKACHIYO.UIViews;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace TAKACHIYO
{
    /// <summary>
    /// <see cref="Actor"/>の状態を表すプレゼンター
    /// </summary>
    public sealed class ActorStatusUIPresenter : MonoBehaviour
    {
        [SerializeField]
        private Define.ActorType target;

        [SerializeField]
        private Image thumbnail;

        [SerializeField]
        private TextMeshProUGUI actorName;

        [SerializeField]
        private Slider hitPointSlider;

        [SerializeField]
        private TextMeshProUGUI hitPointText;

        [SerializeField]
        private string hitPointTextFormat;

        [SerializeField]
        private Transform commandParent;

        [SerializeField]
        private CommandUIPresenter commandUIPresenter;

        [SerializeField]
        private Transform effectParent;
        
        [SerializeField]
        private Transform abnormalStatusIconParent;

        [SerializeField]
        private AbnormalStatusIconUIView abnormalStatusIconUIViewPrefab;

        [SerializeField]
        private AnimationController informationAnimationController;

        [SerializeField]
        private AnimationClip informationDamageAnimation;

        [SerializeField]
        private AnimationClip informationDeadAnimation;

        private readonly Dictionary<Command, CommandUIPresenter> commandUIPresenters = new();

        private readonly Dictionary<Define.AbnormalStatusType, AbnormalStatusIconUIView> abnormalStatusIconUIViews = new();

        private void Start()
        {
            BattleController.Broker.Receive<BattleEvent.SetupBattle>()
                .TakeUntilDestroy(this)
                .Subscribe(x =>
                {
                    var actor = this.target == Define.ActorType.Player ? x.Player : x.Enemy;
                    this.Setup(actor);
                });
        }

        /// <summary>
        /// 利用可能な状態にする
        /// </summary>
        private void Setup(Actor actor)
        {
            this.actorName.text = actor.StatusController.LocalizedName;
            Observable.Merge(
                    actor.StatusController.HitPointMax,
                    actor.StatusController.HitPoint
                    )
                .Subscribe(_ =>
                {
                    this.hitPointSlider.value = actor.StatusController.HitPointRate;
                    this.hitPointText.text = string.Format(
                        this.hitPointTextFormat,
                        actor.StatusController.HitPoint.Value,
                        actor.StatusController.HitPointMax.Value
                        );
                });

            // コマンドの詠唱が開始されたらUIに追加する
            actor.CommandController.CastingCommands
                .ObserveAdd()
                .TakeUntil(BattleController.Broker.Receive<BattleEvent.EndBattle>())
                .Subscribe(x =>
                {
                    var commandUIPresenter = Instantiate(this.commandUIPresenter, this.commandParent);
                    commandUIPresenter.Setup(x.Value);
                    commandUIPresenter.PlayInAnimation();
                    this.commandUIPresenters.Add(x.Value, commandUIPresenter);
                });

            // コマンドの詠唱が終了したらUIも削除する
            actor.CommandController.CastingCommands
                .ObserveRemove()
                .TakeUntil(BattleController.Broker.Receive<BattleEvent.EndBattle>())
                .Subscribe(x =>
                {
                    var commandUIPresenter = this.commandUIPresenters[x.Value];
                    this.commandUIPresenters.Remove(x.Value);
                    commandUIPresenter.PlayOutAnimationAsync()
                        .Subscribe(_ =>
                        {
                            Destroy(commandUIPresenter.gameObject);
                        });
                });

            actor.Broker.Receive<ActorEvent.RequestInstantiateEffect>()
                .TakeUntil(BattleController.Broker.Receive<BattleEvent.EndBattle>())
                .Subscribe(x =>
                {
                    var effect = x.EffectPrefab.Rent();
                    var t = effect.transform;
                    t.SetParent(this.effectParent, false);
                    t.localPosition = Vector3.zero;
                });

            actor.Broker.Receive<ActorEvent.AddedAbnormalStatus>()
                .TakeUntil(BattleController.Broker.Receive<BattleEvent.EndBattle>())
                .Subscribe(x =>
                {
                    var icon = Instantiate(this.abnormalStatusIconUIViewPrefab, this.abnormalStatusIconParent);
                    icon.Setup(BattleSpriteHolder.Instance.GetAbnormalStatusSprite(x.AbnormalStatusType));
                    this.abnormalStatusIconUIViews.Add(x.AbnormalStatusType, icon);
                });

            actor.Broker.Receive<ActorEvent.RemovedAbnormalStatus>()
                .TakeUntil(BattleController.Broker.Receive<BattleEvent.EndBattle>())
                .Subscribe(x =>
                {
                    Destroy(this.abnormalStatusIconUIViews[x.AbnormalStatusType].gameObject);
                    this.abnormalStatusIconUIViews.Remove(x.AbnormalStatusType);
                });

            actor.Broker.Receive<ActorEvent.TakedDamage>()
                .TakeUntil(BattleController.Broker.Receive<BattleEvent.EndBattle>())
                .Where(x => x.Damage > 0)
                .Subscribe(x =>
                {
                    this.informationAnimationController.Play(this.informationDamageAnimation);
                });

            actor.StatusController.HitPoint
                .Where(_ => actor.StatusController.IsDead)
                .Take(1)
                .Subscribe(_ =>
                {
                    this.informationAnimationController.Play(this.informationDeadAnimation);
                });
        }
    }
}
