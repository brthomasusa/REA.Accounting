#pragma warning disable CS8618

using REA.Accounting.SharedKernel;

namespace REA.Accounting.Core.HumanResources.ValueObjects
{
    public class JobTitle
    {
        public string Value { get; }

        protected JobTitle() { }

        private JobTitle(string value) : this()
            => Value = value;

        public static implicit operator string(JobTitle self) => self.Value;


        public static JobTitle Create(string value)
        {
            CheckValidity(value);
            return new JobTitle(value);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The job title is required.");
            }

            if (value.Length > 50)
            {
                throw new ArgumentException("Invalid job title, maximum length is 50 characters.");
            }
        }
    }
}