#pragma warning disable CS8618

using REA.Accounting.SharedKernel.Interfaces;

namespace REA.Accounting.SharedKernel
{
    public abstract class Entity<T>
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        public virtual IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        protected virtual void AddDomainEvent(IDomainEvent newEvent) => _domainEvents.Add(newEvent);

        public virtual void ClearEvents() => _domainEvents.Clear();

        public T Id { get; protected set; }

        public DateTime CreatedDate { get; } = DateTime.UtcNow;

        public DateTime? LastModifiedDate { get; private set; }

        public void UpdateLastModifiedDate()
        {
            LastModifiedDate = DateTime.UtcNow;
        }

        protected virtual void CheckValidity()
        {
            // Validation involving multiple properties go here.
        }
    }
}