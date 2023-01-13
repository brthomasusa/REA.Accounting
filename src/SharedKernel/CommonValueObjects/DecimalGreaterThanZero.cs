using REA.Accounting.SharedKernel.Base;

namespace REA.Accounting.SharedKernel.CommonValueObjects
{
    public class DecimalGreaterThanZero : ValueObject
    {
        public decimal Value { get; }

        protected DecimalGreaterThanZero() { }

        private DecimalGreaterThanZero(decimal amount)
            : this()
        {
            Value = amount;
        }

        public static implicit operator decimal(DecimalGreaterThanZero self) => self.Value;

        public static DecimalGreaterThanZero Create(decimal amount)
        {
            CheckValidity(amount);
            return new DecimalGreaterThanZero(amount);
        }

        private static void CheckValidity(decimal value)
        {
            if (value % 0.01M != 0)
            {
                throw new ArgumentException("The amount can not have more than two decimal places");
            }

            if (value < 0)
            {
                throw new ArgumentException("The amount must be greater than zero ($.00).");
            }
        }
    }
}