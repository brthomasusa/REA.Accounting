using REA.Accounting.SharedKernel.Interfaces;

namespace REA.Accounting.SharedKernel.Base
{
    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot
    {
        private List<DomainEvent>? _domainEvents;
        public IReadOnlyCollection<DomainEvent>? DomainEvents => _domainEvents!.AsReadOnly();

        protected void AddDomainEvent(DomainEvent eventItem)
        {
            _domainEvents ??= new List<DomainEvent>();
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
