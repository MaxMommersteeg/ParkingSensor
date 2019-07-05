using System.Collections.Generic;

namespace App.Core.SharedKernel
{
    public abstract class BaseEntity
    {
        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}
