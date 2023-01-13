using REA.Accounting.SharedKernel.Base;

namespace REA.Accounting.Core.HumanResources.ValueObjects
{
    public class ShiftName : ValueObject
    {
        public string? Value { get; }

        protected ShiftName() { }

        private ShiftName(string name)
            : this() => Value = name;

        public static implicit operator string(ShiftName self) => self.Value!;

        public static ShiftName Create(string value)
        {
            CheckValidity(value);
            return new ShiftName(value);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length > 50)
            {
                throw new ArgumentException("The shift name can not be null or greater than 50 characters.");
            }
        }
    }
}