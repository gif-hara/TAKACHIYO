using TAKACHIYO.BootSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace TAKACHIYO.SceneControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EditEquipmentSceneController : MonoBehaviour
    {
        async void Start()
        {
            await BootSystem.Ready;
        }
    }
}
