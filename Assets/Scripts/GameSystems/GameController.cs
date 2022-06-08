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
        private async void Start()
        {
            await BootSystem.Ready;
        }
    }
}
