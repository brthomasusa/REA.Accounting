using REA.Accounting.SharedKernel.Interfaces;

namespace REA.Accounting.SharedKernel
{
    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot
    {

    }
}
