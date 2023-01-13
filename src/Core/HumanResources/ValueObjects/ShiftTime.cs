using REA.Accounting.SharedKernel.Base;

namespace REA.Accounting.Core.HumanResources.ValueObjects
{
    public class ShiftTime : ValueObject
    {
        public TimeOnly Value { get; }

        protected ShiftTime() { }

        private ShiftTime(int hour, int minute)
            => Value = new TimeOnly(hour, minute);

        public static implicit operator TimeOnly(ShiftTime self) => self.Value!;

        public static ShiftTime Create(int hour, int minute)
        {
            CheckValidity(hour, minute);
            return new ShiftTime(hour, minute);
        }

        private static void CheckValidity(int hour, int minute)
        {
            if (hour < 0 || hour > 23)
                throw new ArgumentException("Valid hour is between 0 and 23.");

            if (minute < 0 || minute > 59)
                throw new ArgumentException("Valid minute is between 0 and 59.");
        }
    }
}