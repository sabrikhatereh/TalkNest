using TalkNest.Core.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TalkNest.Core.Abstractions.Models
{
    public abstract class Aggregate : Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        protected Aggregate(Guid id)
            : base(id)
        {
        }

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents() =>
            _domainEvents.ToList().AsReadOnly();

        public void ClearDomainEvents() =>
            _domainEvents.Clear();

        protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
            _domainEvents.Add(domainEvent);

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}