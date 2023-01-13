using REA.Accounting.SharedKernel.Base;

namespace REA.Accounting.Core.HumanResources.ValueObjects
{
    public class SickLeave : ValueObject
    {
        public int Value { get; }

        protected SickLeave() { }

        private SickLeave(int hours)
            : this()
        {
            Value = hours;
        }

        public static implicit operator int(SickLeave self) => self.Value!;

        public static SickLeave Create(int value)
        {
            CheckValidity(value);
            return new SickLeave(value);
        }

        private static void CheckValidity(int value)
        {
            if (value < 0 || value > 120)
                throw new ArgumentException("Sick leave hours must be between 0 and 120");
        }
    }
}