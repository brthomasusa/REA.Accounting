#pragma warning disable CS8600, CS8604, CS8765

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using REA.Accounting.Infrastructure.Persistence.DataModels.Sales;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;

namespace REA.Accounting.UnitTests.Data
{
    public static class LoadTestData
    {
        static readonly string BaseFilePath;

        static LoadTestData()
        {
            // An alternative method of adding a custom JsonConverter
            // JsonConvert.DefaultSettings = () => new SerializerSettings();
            BaseFilePath = "../../../Data/";
        }

        public static async Task<HashSet<BusinessEntity>> LoadBusinessEntityDataAsync()
        {
            Task<HashSet<BusinessEntity>> getTask = Task.Run(() => LoadTestData.LoadBusinessEntityData())
                                                        .ContinueWith(antecedent => antecedent.Result);

            return await Task.FromResult(getTask.Result);
        }

        public static async Task<HashSet<CountryRegion>> LoadCountryRegionDataAsync()
        {
            Task<HashSet<CountryRegion>> getTask = Task.Run(() => LoadTestData.LoadCountryRegionData())
                                                       .ContinueWith(antecedent => antecedent.Result);

            return await Task.FromResult(getTask.Result);
        }

        public static async Task<HashSet<SalesTerritory>> LoadSalesTerritoryDataAsync()
        {
            Task<HashSet<SalesTerritory>> getTask = Task.Run(() => LoadTestData.LoadSalesTerritoryData())
                                                                      .ContinueWith(antecedent => antecedent.Result);

            return await Task.FromResult(getTask.Result);
        }

        public static async Task<HashSet<StateProvince>> LoadStateProvinceDataAsync()
        {
            Task<HashSet<StateProvince>> getTask = Task.Run(() => LoadTestData.LoadStateProvinceData())
                                                                    .ContinueWith(antecedent => antecedent.Result);

            return await Task.FromResult(getTask.Result);
        }

        public static async Task<HashSet<Address>> LoadAddressDataAsync()
        {
            Task<HashSet<Address>> getTask = Task.Run(() => LoadTestData.LoadAddressData())
                                                 .ContinueWith(antecedent => antecedent.Result);

            return await Task.FromResult(getTask.Result);
        }

        public static async Task<HashSet<PersonDataModel>> LoadPersonDataAsync()
        {
            Task<HashSet<PersonDataModel>> getTask = Task.Run(() => LoadTestData.LoadPersonData())
                                                         .ContinueWith(antecedent => antecedent.Result);

            return await Task.FromResult(getTask.Result);
        }

        public static async Task<HashSet<BusinessEntityAddress>> LoadBusinessEntityAddressDataAsync()
        {
            Task<HashSet<BusinessEntityAddress>> getTask = Task.Run(() => LoadTestData.LoadBusinessEntityAddressData())
                                                               .ContinueWith(antecedent => antecedent.Result);

            return await Task.FromResult(getTask.Result);
        }

        public static async Task<HashSet<EmailAddress>> LoadEmailAddressDataAsync()
        {
            Task<HashSet<EmailAddress>> getTask = Task.Run(() => LoadTestData.LoadEmailAddressData())
                                                      .ContinueWith(antecedent => antecedent.Result);

            return await Task.FromResult(getTask.Result);
        }

        public static async Task<HashSet<PersonPhone>> LoadPhoneDataAsync()
        {
            Task<HashSet<PersonPhone>> getTask = Task.Run(() => LoadTestData.LoadPhoneData())
                                                     .ContinueWith(antecedent => antecedent.Result);

            return await Task.FromResult(getTask.Result);
        }

        public static async Task<HashSet<EmployeeDataModel>> LoadEmployeeDataAsync()
        {
            Task<HashSet<EmployeeDataModel>> getTask = Task.Run(() => LoadTestData.LoadEmployeeData())
                                                  .ContinueWith(antecedent => antecedent.Result);

            return await Task.FromResult(getTask.Result);
        }

        public static async Task<HashSet<EmployeeDepartmentHistory>> LoadEmployeeDepartmentHistoryDataAsync()
        {
            Task<HashSet<EmployeeDepartmentHistory>> getTask = Task.Run(() => LoadTestData.LoadDepartmentHistoryData())
                                                                   .ContinueWith(antecedent => antecedent.Result);

            return await Task.FromResult(getTask.Result);
        }

        public static async Task<HashSet<EmployeePayHistory>> LoadEmployeePayHistoryDataAsync()
        {
            Task<HashSet<EmployeePayHistory>> getTask = Task.Run(() => LoadTestData.LoadPayHistoryData())
                                                            .ContinueWith(antecedent => antecedent.Result);

            return await Task.FromResult(getTask.Result);
        }

        /* --------------------------------------------------------------------------------------------- */

        private static HashSet<EmployeeDataModel> LoadEmployeeData()
        {
            string fileName = $"{BaseFilePath}Employee-XS.json";
            string jsonString = File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<HashSet<EmployeeDataModel>>(jsonString)!;
        }

        private static HashSet<BusinessEntity> LoadBusinessEntityData()
        {
            string fileName = $"{BaseFilePath}BusinessEntity-XS.json";
            string jsonString = File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<HashSet<BusinessEntity>>(jsonString)!;
        }

        private static HashSet<AddressType> LoadAddressTypeData()
        {
            string fileName = $"{BaseFilePath}AddressType.json";
            string jsonString = File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<HashSet<AddressType>>(jsonString)!;
        }

        private static HashSet<CountryRegion> LoadCountryRegionData()
        {
            string fileName = $"{BaseFilePath}CountryRegion.json";
            string jsonString = File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<HashSet<CountryRegion>>(jsonString)!;
        }

        private static HashSet<SalesTerritory> LoadSalesTerritoryData()
        {
            string fileName = $"{BaseFilePath}SalesTerritory.json";
            string jsonString = File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<HashSet<SalesTerritory>>(jsonString)!;
        }

        private static HashSet<StateProvince> LoadStateProvinceData()
        {
            string fileName = $"{BaseFilePath}StateProvince.json";
            string jsonString = File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<HashSet<StateProvince>>(jsonString)!;
        }

        private static HashSet<Address> LoadAddressData()
        {
            string fileName = $"{BaseFilePath}Address-XS.json";
            string jsonString = File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<HashSet<Address>>(jsonString)!;
        }

        private static HashSet<PersonDataModel> LoadPersonData()
        {
            string fileName = $"{BaseFilePath}Person-XS.json";
            string jsonString = File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<HashSet<PersonDataModel>>(jsonString)!;
        }

        private static HashSet<BusinessEntityAddress> LoadBusinessEntityAddressData()
        {
            string fileName = $"{BaseFilePath}BusinessEntityAddress-XS.json";
            string jsonString = File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<HashSet<BusinessEntityAddress>>(jsonString)!;
        }

        private static HashSet<EmailAddress> LoadEmailAddressData()
        {
            string fileName = $"{BaseFilePath}EmailAddress-XS.json";
            string jsonString = File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<HashSet<EmailAddress>>(jsonString, new EmailAddressConverter())!;
        }

        private static HashSet<PersonPhone> LoadPhoneData()
        {
            string fileName = $"{BaseFilePath}Telephone-XS.json";
            string jsonString = File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<HashSet<PersonPhone>>(jsonString)!;
        }

        private static async Task<HashSet<EmployeeDepartmentHistory>> LoadDepartmentHistoryData()
        {
            string fileName = $"{BaseFilePath}EmployeeDepartmentHistory-XS.json";
            string jsonString = File.ReadAllText(fileName);

            return await Task.FromResult(JsonConvert.DeserializeObject<HashSet<EmployeeDepartmentHistory>>(jsonString)!);
        }

        private static async Task<HashSet<EmployeePayHistory>> LoadPayHistoryData()
        {
            string fileName = $"{BaseFilePath}EmployeePayHistory-XS.json";
            string jsonString = File.ReadAllText(fileName);

            return await Task.FromResult(JsonConvert.DeserializeObject<HashSet<EmployeePayHistory>>(jsonString)!);
        }

        /*  ================================================================================================  */

        public static async Task<List<PersonPhone>> LoadTelephoneDataAsync()
        {
            const string fileName = "/home/bthomas/Projects/NetCore/REA.Accounting/test/UnitTests/Data/Telephone-SM.json";
            string jsonString = File.ReadAllText(fileName);

            return await Task.FromResult(JsonConvert.DeserializeObject<List<PersonPhone>>(jsonString)!);
        }

        public static async Task<HashSet<AddressType>> LoadAddressTypeDataAsync()
        {
            Task<HashSet<AddressType>> getAddressTypeTask = Task.Run(() => LoadTestData.LoadAddressTypeData())
                                                                .ContinueWith(antecedent => antecedent.Result);

            return await Task.FromResult(getAddressTypeTask.Result);
        }

        public static HashSet<EmailAddress> GetEmailAddressData()
        {
            string fileName = $"{BaseFilePath}EmailAddress-XS.json";
            string jsonString = File.ReadAllText(fileName);
            HashSet<EmailAddress> result = JsonConvert.DeserializeObject<HashSet<EmailAddress>>(jsonString)!;

            return result;
        }
    }

    public class EmailAddressConverter : JsonCreationConverter<EmailAddress>
    {
        protected override EmailAddress Create(Type objectType, JObject jObject)
            => new()
            {
                BusinessEntityID = (int)jObject["BusinessEntityID"],
                EmailAddressID = (int)jObject["EmailAddressID"],
                MailAddress = jObject["EmailAddress"]!.ToString(),
                RowGuid = new Guid(jObject["rowguid"]!.ToString()),
                ModifiedDate = DateTime.Parse(jObject["ModifiedDate"]!.ToString())
            };
    }

    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        /// <summary>
        /// Create an instance of objectType, based properties in the JSON object
        /// </summary>
        /// <param name="objectType">type of object expected</param>
        /// <param name="jObject">
        /// contents of JSON object that will be deserialized
        /// </param>
        /// <returns></returns>
        protected abstract T Create(Type objectType, JObject jObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson
        (
            JsonWriter writer,
            Object value,
            JsonSerializer serializer
        )
        {
        }

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                         object existingValue,
                                         JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            T target = Create(objectType, jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
    }

    public sealed class SerializerSettings : JsonSerializerSettings
    {
        public SerializerSettings() : base()
        {
            this.Converters.Add(new EmailAddressConverter());
        }
    }
}