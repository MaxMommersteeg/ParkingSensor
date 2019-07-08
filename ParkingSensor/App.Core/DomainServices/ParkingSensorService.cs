using System;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Messages.Commands;
using App.Core.Messages.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.Core.DomainServices
{
    public class ParkingSensorService : IParkingSensorService
    {
        private static readonly TimeSpan ToneDuration = TimeSpan.FromMilliseconds(50);

        private readonly ILogger _logger;
        private readonly IMediator _messagingMediator;
        private readonly IDistanceToToneFrequencyConverter _distanceToToneFrequencyConverter;

        public ParkingSensorService(
            ILogger<ParkingSensorService> logger,
            IMediator messagingMediator,
            IDistanceToToneFrequencyConverter distanceToToneFrequencyConverter)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _messagingMediator = messagingMediator ?? throw new ArgumentNullException(nameof(messagingMediator));
            _distanceToToneFrequencyConverter = distanceToToneFrequencyConverter ?? throw new ArgumentNullException(nameof(distanceToToneFrequencyConverter));
        }

        public Task Handle(DistanceMeasured distanceMeasured, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Received {distanceMeasured.GetType()} event.");

            var frequencyToPlay = _distanceToToneFrequencyConverter.DistanceToFrequency(distanceMeasured.Distance);
            var command = new PlayTone(frequencyToPlay, ToneDuration);
            _messagingMediator.Send(command);

            _logger.LogDebug($"Sent {command.GetType()} command.");

            return Unit.Task;
        }
    }
}
