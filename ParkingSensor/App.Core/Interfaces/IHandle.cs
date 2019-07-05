using App.Core.SharedKernel;

namespace App.Core.Interfaces
{
    public interface IHandle<T>
        where T : BaseDomainEvent
    {
        void Handle(T @event);
    }
}
