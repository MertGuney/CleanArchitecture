using CleanArchitecture.Domain.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.Common
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; }

        private readonly List<BaseEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);
        public void RemoveDomainEvent(BaseEvent domainEvent) => _domainEvents.Remove(domainEvent);
        public void ClearDomainEvent() => _domainEvents.Clear();
    }
}
