using System;
using App.Core.Messages.Commands;
using MediatR;

namespace App.Core.DomainServices
{
    public interface IDistanceMeasurementService :
        IRequestHandler<StartDistanceMeasurement>,
        IRequestHandler<StopDistanceMeasurement>,
        IDisposable
    {
        bool Started { get; }
    }
}
