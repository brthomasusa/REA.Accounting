using MediatR;

namespace REA.Accounting.SharedKernel.Base
{
    public abstract record DomainEvent : INotification;
}
