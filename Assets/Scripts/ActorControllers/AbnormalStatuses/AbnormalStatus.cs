using UniRx;

namespace TAKACHIYO.ActorControllers.AbnormalStatuses
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbnormalStatus : IAbnormalStatus
    {
        protected readonly CompositeDisposable disposable = new CompositeDisposable();
        
        public abstract void Setup(Actor owner);
        
        public void Dispose()
        {
            disposable?.Dispose();
        }
    }
}
