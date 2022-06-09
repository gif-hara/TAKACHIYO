using TAKACHIYO.ActorControllers;

namespace TAKACHIYO.CommandSystems.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommandAction
    {
        void Invoke(Actor owner);
    }
}
