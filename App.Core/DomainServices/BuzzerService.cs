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

        public Task<Unit> Handle(PlaySoundEffect request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Received {request} command.");

            _buzzer.PlaySoundEffect(request.Frequency, request.Duration, request.Beeps);

            _logger.LogInformation($"Played tone.");

            return Unit.Task;
        }

        public void Dispose()
        {
            _buzzer?.Dispose();
        }
    }
}
