using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Guards;

namespace REA.Accounting.Core.HumanResources.ValueObjects
{
    public class RateOfPay : ValueObject
    {
        public decimal Value { get; }

        protected RateOfPay() { }

        private RateOfPay(decimal rate)
            : this()
        {
            Value = rate;
        }

        public static implicit operator decimal(RateOfPay self) => self.Value!;

        public static RateOfPay Create(decimal value)
        {
            CheckValidity(value);
            return new RateOfPay(value);
        }

        private static void CheckValidity(decimal value)
        {
            Guard.Against.LessThan(value, 6.50M, "Rate", "The minimum rate of pay is $6.50 per hour.");
            Guard.Against.GreaterThan(value, 200.00M, "Rate", "The maximum rate of pay is $6.50 per hour.");
        }
    }
}