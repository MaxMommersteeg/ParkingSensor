using System;

namespace App.Core.SharedKernel
{
    public abstract class BaseDomainEvent
    {
        public DateTime Timestamp { get; protected set; } = DateTime.UtcNow;
    }
}
