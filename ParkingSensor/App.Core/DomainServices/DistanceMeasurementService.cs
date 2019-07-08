﻿using System;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Devices;
using App.Core.Messages.Commands;
using App.Core.Messages.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.Core.DomainServices
{
    public class DistanceMeasurementService : IDistanceMeasurementService
    {
        private static readonly TimeSpan MinimalInterval = TimeSpan.FromMilliseconds(60);

        private readonly ILogger _logger;
        private readonly IMediator _messagingMediator;
        private readonly IMeasureSensor _measureSensor;

        private Timer _timer;

        public DistanceMeasurementService(
            ILogger<DistanceMeasurementService> logger,
            IMediator messagingMediator,
            IMeasureSensor measureSensor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _messagingMediator = messagingMediator ?? throw new ArgumentNullException(nameof(messagingMediator));
            _measureSensor = measureSensor ?? throw new ArgumentNullException(nameof(measureSensor));
        }

        public bool Started { get; private set; }

        public Task<Unit> Handle(StartDistanceMeasurement request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Received {request.GetType()} command.");

            Console.WriteLine($"({nameof(DistanceMeasurementService)}) Started distance measurement service.");

            Started = true;
            _timer = new Timer(OnIntervalElapsed, null, TimeSpan.Zero, GetValueOrMinimalInterval(request.MeasureInterval));

            _logger.LogDebug($"Started distance measuring.");

            return Unit.Task;
        }

        public Task<Unit> Handle(StopDistanceMeasurement request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Received {request.GetType()} command.");

            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            Started = false;

            _logger.LogDebug($"Stopped distance measuring.");

            return Unit.Task;
        }

        public void Dispose()
        {
            _measureSensor?.Dispose();
        }

        private void OnIntervalElapsed(object state)
        {
            var distance = _measureSensor.GetDistance();

            _logger.LogInformation($"Measured distance. {distance}cm");

            var @event = new DistanceMeasured(distance);
            _messagingMediator.Publish(@event);

            _logger.LogInformation($"Published {@event.GetType()} event.");
        }

        private TimeSpan GetValueOrMinimalInterval(TimeSpan? timeSpan)
        {
            if (!timeSpan.HasValue)
            {
                return MinimalInterval;
            }

            return timeSpan.Value >= MinimalInterval ? timeSpan.Value : MinimalInterval;
        }
    }
}
