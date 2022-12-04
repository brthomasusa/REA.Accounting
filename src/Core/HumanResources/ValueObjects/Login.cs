#pragma warning disable CS8618

using REA.Accounting.SharedKernel;

namespace REA.Accounting.Core.HumanResources.ValueObjects
{
    public class Login : ValueObject
    {
        public string Value { get; }

        protected Login() { }

        private Login(string value) : this()
            => Value = value;

        public static implicit operator string(Login self) => self.Value;

        public static Login Create(string login)
        {
            CheckValidity(login);
            return new Login(login);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The login id is required.");
            }

            if (value.Length > 256)
            {
                throw new ArgumentException("Invalid login id, maximum length is 256 characters.");
            }
        }
    }
}