using TAKACHIYO.BootSystems;
using TAKACHIYO.MasterDataSystems;
using TMPro;
using UnityEngine;

namespace TAKACHIYO
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GameController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI test;
        
        private async void Start()
        {
            await BootSystem.Ready;

            this.test.text = MasterDataActorStatus.Get("1").localizedName.GetLocalizedString();
        }
    }
}
