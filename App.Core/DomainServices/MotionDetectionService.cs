using System;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Devices;
using App.Core.Messages.Commands;
using App.Core.Messages.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.Core.DomainServices
{
    public class MotionDetectionService : IMotionDetectionService
    {
        private const double HighSoundFrequency = 1650;

        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IMotionDetectionSensor _motionDetectionSensor;

        public MotionDetectionService(
            ILogger<MotionDetectionService> logger,
            IMediator mediator,
            IMotionDetectionSensor motionDetectionSensor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _motionDetectionSensor = motionDetectionSensor ?? throw new ArgumentNullException(nameof(motionDetectionSensor));
        }

        public bool Started { get; private set; }

        public Task<Unit> Handle(StartMotionDetection request, CancellationToken cancellationToken)
        {
            Started = true;
            return Unit.Task;
        }

        public async Task Handle(MotionDetected motionDetected, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Received {motionDetected} event.");

            if (!Started)
            {
                return;
            }

            var command = new StartPlayingSoundEffect(HighSoundFrequency);
            await _mediator.Send(command);
        }

        public async Task Handle(MotionStopped motionStopped, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Received {motionStopped} event.");

            if (!Started)
            {
                return;
            }

            var command = new StopPlayingSoundEffect();
            await _mediator.Send(command);
        }

        public void Dispose()
        {
            _motionDetectionSensor?.Dispose();
        }
    }
}
