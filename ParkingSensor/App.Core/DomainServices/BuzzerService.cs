using System.Threading;
using System.Threading.Tasks;
using App.Core.Messages.Commands;
using MediatR;

namespace App.Core.DomainServices
{
    public class BuzzerService : IRequestHandler<PlayTone>, IBuzzerService
    {
        public Task<Unit> Handle(PlayTone request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
