using System;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Messages.Commands;
using App.Core.Messages.Events;
using MediatR;

namespace App.Core.DomainServices
{
    public class ParkingSensorService :
        IRequestHandler<StartParkingService>,
        IRequestHandler<StopParkingService>,
        INotificationHandler<DistanceMeasured>,
        IParkingSensorService
    {
        private static readonly TimeSpan ToneDuration = TimeSpan.FromMilliseconds(50);

        private readonly IMediator _messagingMediator;
        private readonly IDistanceToToneFrequencyConverter _distanceToToneFrequencyConverter;

        public ParkingSensorService(
            IMediator messagingMediator,
            IDistanceToToneFrequencyConverter distanceToToneFrequencyConverter)
        {
            _messagingMediator = messagingMediator;
            _distanceToToneFrequencyConverter = distanceToToneFrequencyConverter;
        }

        public Task<Unit> Handle(StartParkingService request, CancellationToken cancellationToken)
        {
            Start();
            return Unit.Task;
        }

        public Task<Unit> Handle(StopParkingService request, CancellationToken cancellationToken)
        {
            Stop();
            return Unit.Task;
        }

        public Task Handle(DistanceMeasured distanceMeasured, CancellationToken cancellationToken)
        {
            if (!Started)
            {
                return Task.CompletedTask;
            }

            var frequencyToPlay = _distanceToToneFrequencyConverter.DistanceToFrequency(distanceMeasured.Distance);
            return _messagingMediator.Send(new PlayTone(frequencyToPlay, ToneDuration));
        }

        public bool Started { get; private set; }

        private void Start()
        {
            Started = true;
        }

        private void Stop()
        {
            Started = false;
        }
    }
}
