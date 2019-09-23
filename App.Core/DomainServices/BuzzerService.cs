using System;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Devices;
using App.Core.Messages.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.Core.DomainServices
{
    public class BuzzerService : IBuzzerService
    {
        private readonly ILogger _logger;
        private readonly IBuzzer _buzzer;

        public BuzzerService(
            ILogger<BuzzerService> logger,
            IBuzzer buzzer)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _buzzer = buzzer ?? throw new ArgumentNullException(nameof(buzzer));
        }

        public Task<Unit> Handle(StartPlayingSoundEffect request, CancellationToken cancellationToken)
        {
            _buzzer.StartPlaying(request.Frequency);

            _logger.LogInformation("Sent start playing request.");

            return Unit.Task;
        }

        public Task<Unit> Handle(StopPlayingSoundEffect request, CancellationToken cancellationToken)
        {
            _buzzer.StopPlaying();

            _logger.LogInformation("Sent stop playing request.");

            return Unit.Task;
        }

        public void Dispose()
        {
            _buzzer?.Dispose();
        }
    }
}
