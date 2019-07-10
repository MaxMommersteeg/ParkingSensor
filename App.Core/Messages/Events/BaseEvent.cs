using System;
using MediatR;

namespace App.Core.Messages.Events
{
    public abstract class BaseEvent : INotification
    {
        public DateTime Timestamp { get; protected set; } = DateTime.UtcNow;
    }
}
