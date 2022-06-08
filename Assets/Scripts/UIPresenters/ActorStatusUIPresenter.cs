using System;
using TAKACHIYO.ActorControllers;
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

        private void Start()
        {
            GameController.Broker.Receive<GameEvent.OnSetupBattle>()
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
        }
    }
}
