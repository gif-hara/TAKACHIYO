using System.Collections.Generic;
using TAKACHIYO.ActorControllers;
using UnityEngine.Assertions;

namespace TAKACHIYO
{
    /// <summary>
    /// 
    /// </summary>
    public static class ActorExtensions
    {
        public static List<Actor> GetTargets(this Actor self, Define.TargetType targetType)
        {
            var result = new List<Actor>();
            switch (targetType)
            {

                case Define.TargetType.My:
                    result.Add(self);
                    break;
                case Define.TargetType.Opponent:
                    result.Add(self.Opponent);
                    break;
                case Define.TargetType.All:
                    result.Add(self);
                    result.Add(self.Opponent);
                    break;
                default:
                    Assert.IsTrue(false, $"{targetType}は未対応です");
                    break;
            }

            return result;
        }
    }
}
