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
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IDistanceToSoundEffectConverter _distanceToSoundEffectConverter;

        public ParkingSensorService(
            ILogger<ParkingSensorService> logger,
            IMediator mediator,
            IDistanceToSoundEffectConverter distanceToSoundEffectConverter)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _distanceToSoundEffectConverter = distanceToSoundEffectConverter ?? throw new ArgumentNullException(nameof(distanceToSoundEffectConverter));
        }

        public async Task Handle(DistanceMeasured distanceMeasured, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Received {distanceMeasured} event.");

            var soundEffect = _distanceToSoundEffectConverter.DistanceToSoundEffect(distanceMeasured.Distance);

            IRequest command;
            if (soundEffect == null)
            {
                command = new StopPlayingSoundEffect();
            }
            else
            {
                command = new StartPlayingSoundEffect(soundEffect.Frequency);
            }

            await _mediator.Send(command);

            _logger.LogDebug($"Sent {command} command.");
        }
    }
}
