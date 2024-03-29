using REA.Accounting.SharedKernel.Base;

namespace REA.Accounting.SharedKernel.CommonValueObjects
{
    public class Status : ValueObject
    {
        public bool Value { get; }

        protected Status() { }

        private Status(bool value) : this() => Value = value;

        public static implicit operator bool(Status self) => self.Value;

        public static Status Create(bool status)
        {
            return new Status(status);
        }
    }
}