namespace REA.Accounting.SharedKernel.CommonValueObjects
{
    public class EntityDate : ValueObject
    {
        public DateTime Value { get; }

        protected EntityDate() { }

        private EntityDate(DateTime entityDate)
            : this()
        {
            Value = entityDate;
        }

        public static implicit operator DateTime(EntityDate self) => self.Value;

        public static EntityDate Create(DateTime entityDate)
        {
            CheckValidity(entityDate);
            return new EntityDate(entityDate);
        }

        private static void CheckValidity(DateTime value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The date is required.");
            }
        }
    }
}