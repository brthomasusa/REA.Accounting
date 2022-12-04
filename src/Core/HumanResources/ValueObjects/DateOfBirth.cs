using REA.Accounting.SharedKernel;

namespace REA.Accounting.Core.HumanResources.ValueObjects
{
    public class DateOfBirth
    {
        public DateOnly Value { get; }

        protected DateOfBirth() { }

        private DateOfBirth(DateOnly birthDate)
            : this()
        {
            Value = birthDate;
        }

        public static implicit operator DateOnly(DateOfBirth self) => self.Value!;

        public static DateOfBirth Create(DateOnly birthDate)
        {
            CheckValidity(birthDate);
            return new DateOfBirth(birthDate);
        }

        private static void CheckValidity(DateOnly value)
        {
            DateTime temp = DateTime.Now;
            DateOnly today = new(temp.Year, temp.Month, temp.Day);

            if (value < new DateOnly(1930, 1, 1) || value > today.AddYears(-18))
            {
                throw new ArgumentException($"Birth date must be between 1930-1-1 and {today.AddYears(-18).ToShortDateString}");
            }
        }
    }
}