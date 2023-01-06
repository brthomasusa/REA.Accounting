using REA.Accounting.SharedKernel;
using REA.Accounting.SharedKernel.Guards;

namespace REA.Accounting.Core.HumanResources.ValueObjects
{
    public class DateOfRateChange : ValueObject
    {
        public DateTime Value { get; }

        protected DateOfRateChange() { }

        private DateOfRateChange(DateTime rateChangeDate)
            : this()
        {
            Value = rateChangeDate;
        }

        public static implicit operator DateTime(DateOfRateChange self) => self.Value!;

        public static DateOfRateChange Create(DateTime value)
        {
            CheckValidity(value);
            return new DateOfRateChange(value);
        }

        private static void CheckValidity(DateTime value)
        {
            Guard.Against.DefaultDateTime(value, "RateChangeDate", "The date the rate of pay was set is required.");
        }
    }
}