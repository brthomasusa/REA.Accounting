using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Guards;

namespace REA.Accounting.Core.HumanResources.ValueObjects
{
    public class DepartmentStartDate : ValueObject
    {
        public DateOnly Value { get; }

        protected DepartmentStartDate() { }

        private DepartmentStartDate(DateOnly startDate)
            : this()
        {
            Value = startDate;
        }

        public static implicit operator DateOnly(DepartmentStartDate self) => self.Value!;

        public static DepartmentStartDate Create(DateOnly value)
        {
            CheckValidity(value);
            return new DepartmentStartDate(value);
        }

        private static void CheckValidity(DateOnly value)
        {
            Guard.Against.DefaultDateTime(value, "StartDate", "The date the employee was assigned to the department is required.");
        }


    }
}