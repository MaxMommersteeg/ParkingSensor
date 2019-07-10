using System;
using MediatR;

namespace App.Core.Messages.Commands
{
    public class BaseCommand : IRequest
    {
        public DateTime Timestamp { get; protected set; } = DateTime.UtcNow;
    }
}
