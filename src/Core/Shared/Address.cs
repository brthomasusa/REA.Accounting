#pragma warning disable CS8618

using REA.Accounting.Core.Shared.ValueObjects;
using REA.Accounting.SharedKernel;
using ValueObject = REA.Accounting.Core.Shared.ValueObjects;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Core.Shared
{
    public class Address : Entity<int>
    {
        protected Address() { }

        private Address
        (
            int addressID,
            int businessEntityID,
            AddressTypeEnum addressType,
            AddressLine1 line1,
            AddressLine2? line2,
            City city,
            int stateProvinceID,
            PostalCode postalCode
        ) : this()
        {
            Id = addressID;
            BusinessEntityID = businessEntityID;
            AddressType = addressType;
            AddressLine1 = line1.Value!;
            AddressLine2 = line2!.Value;
            City = city.Value!;
            StateProvinceId = stateProvinceID;
            PostalCode = postalCode.Value!;
        }

        internal static Address Create
        (
            int addressID,
            int businessEntityID,
            AddressTypeEnum addressType,
            string line1,
            string? line2,
            string city,
            int stateProvinceID,
            string postalCode
        )
        {
            return new Address
            (
                addressID,
                businessEntityID,
                Enum.IsDefined(typeof(AddressTypeEnum), addressType) ? addressType : throw new ArgumentException("Invalid address type."),
                ValueObject.AddressLine1.Create(line1),
                ValueObject.AddressLine2.Create(line2!),
                ValueObject.City.Create(city),
                (stateProvinceID > 0 ? stateProvinceID : throw new ArgumentNullException("A state/province id is required.")),
                ValueObject.PostalCode.Create(postalCode)
            );
        }

        public int BusinessEntityID { get; private set; }
        public AddressTypeEnum AddressType { get; private set; }
        public string AddressLine1 { get; private set; }
        public string? AddressLine2 { get; private set; }
        public string City { get; private set; }
        public int StateProvinceId { get; private set; }
        public string PostalCode { get; private set; }

        internal OperationResult<Address> Update
        (

            AddressTypeEnum addressType,
            string line1,
            string? line2,
            string city,
            int stateProvinceID,
            string postalCode
        )
        {
            try
            {
                AddressType = Enum.IsDefined(typeof(AddressTypeEnum), addressType) ? addressType : throw new ArgumentException("Invalid address type.");
                AddressLine1 = ValueObject.AddressLine1.Create(line1).Value!;
                AddressLine2 = ValueObject.AddressLine2.Create(line2!).Value!;
                City = ValueObject.City.Create(city).Value!;
                StateProvinceId = (stateProvinceID > 0 ? stateProvinceID : throw new ArgumentNullException("A state/province id is required."));
                PostalCode = ValueObject.PostalCode.Create(postalCode).Value!;

                UpdateModifiedDate();

                return OperationResult<Address>.CreateSuccessResult(this);

            }
            catch (Exception ex)
            {
                return OperationResult<Address>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }
    }

    public enum AddressTypeEnum : int
    {
        Billing = 1,
        Home = 2,
        MainOffice = 3,
        Primary = 4,
        Shipping = 5,
        Archive = 6
    }
}