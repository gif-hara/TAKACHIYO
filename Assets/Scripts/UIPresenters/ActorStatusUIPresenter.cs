using System;
using System.Collections.Generic;
using TAKACHIYO.ActorControllers;
using TAKACHIYO.BattleSystems;
using TAKACHIYO.CommandSystems;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UniRx;

namespace TAKACHIYO
{
    /// <summary>
    /// 
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

        private readonly Dictionary<Command, CommandUIPresenter> commandUIPresenters = new();

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

            actor.CommandController.CastingCommands
                .ObserveAdd()
                .TakeUntil(BattleController.Broker.Receive<BattleEvent.EndBattle>())
                .Subscribe(x =>
                {
                    var commandUIPresenter = Instantiate(this.commandUIPresenter, this.commandParent);
                    commandUIPresenter.Setup(x.Value);
                    this.commandUIPresenters.Add(x.Value, commandUIPresenter);
                });

            actor.CommandController.CastingCommands
                .ObserveRemove()
                .TakeUntil(BattleController.Broker.Receive<BattleEvent.EndBattle>())
                .Subscribe(x =>
                {
                    var commandUIPresenter = this.commandUIPresenters[x.Value];
                    Destroy(commandUIPresenter.gameObject);
                    this.commandUIPresenters.Remove(x.Value);
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
        }
    }
}
