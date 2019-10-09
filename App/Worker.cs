using System;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Messages.Commands;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace App
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMediator _mediator;

        public Worker(
            ILogger<Worker> logger,
            IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Started Parking Sensor background service.");

            var startDistanceMeasurement = _mediator.Send(new StartDistanceMeasurement(TimeSpan.FromSeconds(1)));
            var startMotionDetection = _mediator.Send(new StartMotionDetection());

            await Task.WhenAll(startDistanceMeasurement, startMotionDetection);
        }
    }
}
