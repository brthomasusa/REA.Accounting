namespace REA.Accounting.SharedKernel.CommonValueObjects
{
    public class EntityGuidID : ValueObject
    {
        public Guid Value { get; }

        protected EntityGuidID() { }

        private EntityGuidID(Guid id)
            : this()
        {
            Value = id;
        }

        public static implicit operator Guid(EntityGuidID self) => self.Value;

        public static EntityGuidID Create(Guid id)
        {
            CheckValidity(id);
            return new EntityGuidID(id);
        }

        private static void CheckValidity(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The entity Id is required; it can not be a default Guid.", nameof(value));
            }
        }
    }
}