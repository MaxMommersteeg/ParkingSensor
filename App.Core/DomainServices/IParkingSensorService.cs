using App.Core.Messages.Events;
using MediatR;

namespace App.Core.DomainServices
{
    public interface IParkingSensorService :
        INotificationHandler<DistanceMeasured>
    {
    }
}
