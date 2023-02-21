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

        private static void CheckValidity(string jobTitle)
        {
            if (string.IsNullOrEmpty(jobTitle))
            {
                throw new ArgumentNullException(nameof(jobTitle), "The job title is required.");
            }

            if (jobTitle.Length > 50)
            {
                throw new ArgumentException("Invalid job title, maximum length is 50 characters.");
            }
        }
    }
}