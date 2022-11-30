using REA.Accounting.SharedKernel;

namespace REA.Accounting.Core.Shared.ValueObjects
{
    public class Title : ValueObject
    {
        public string? Value { get; }

        protected Title() { }

        private Title(string title)
            : this() => Value = title;

        public static implicit operator string(Title self) => self.Value!;

        public static Title Create(string value)
        {
            CheckValidity(value);
            return new Title(value);
        }

        private static void CheckValidity(string value)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > 8)
            {
                throw new ArgumentException("Title can not be greater than 8 characters.");
            }
        }
    }
}