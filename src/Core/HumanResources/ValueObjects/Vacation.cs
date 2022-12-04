using REA.Accounting.SharedKernel;

namespace REA.Accounting.Core.HumanResources.ValueObjects
{
    public class Vacation : ValueObject
    {
        public int Value { get; }

        protected Vacation() { }

        private Vacation(int hours)
            : this()
        {
            Value = hours;
        }

        public static implicit operator int(Vacation self) => self.Value!;

        public static Vacation Create(int value)
        {
            CheckValidity(value);
            return new Vacation(value);
        }

        private static void CheckValidity(int value)
        {
            if (value < -40 || value > 240)
                throw new ArgumentException("Vacation hours must be between -40 and 240");
        }
    }
}