using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.SharedKernel.Interfaces
{
    public interface IBusinessRule<T>
    {
        void SetNext(IBusinessRule<T> next);

        Task<ValidationResult> Validate(T request);
    }
}