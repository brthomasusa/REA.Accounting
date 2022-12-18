using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using REA.Accounting.Infrastructure.Persistence.DataModels.Sales;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;


namespace REA.Accounting.UnitTests.Data
{
    public static class LoadTestData
    {
        static readonly string BaseFilePath = "../../../Data/";

        public static async Task LoadAllData()
        {
            Task<List<PersonPhone>> getPhonesTask = Task.Run(() => LoadTestData.LoadTelephoneData())
                                                        .ContinueWith(antecedent => antecedent.Result);

            Task<HashSet<Employee>> getEmployeeTask = Task.Run(() => LoadTestData.LoadEmployeeData())
                                                       .ContinueWith(antecedent => antecedent.Result);

            List<PersonPhone> phoneResult = await Task.FromResult(getPhonesTask.Result);
            HashSet<Employee> employeeResult = await Task.FromResult(getEmployeeTask.Result);
        }

        public static async Task<HashSet<BusinessEntity>> LoadBusinessEntityDataAsync()
        {
            Task<HashSet<BusinessEntity>> getTask = Task.Run(() => LoadTestData.LoadBusinessEntityData())
                                                     .ContinueWith(antecedent => antecedent.Result);

            return await Task.FromResult(getTask.Result);
        }

        public static async Task<HashSet<Employee>> LoadEmployeeDataAsync()
        {
            Task<HashSet<Employee>> getEmployeeTask = Task.Run(() => LoadTestData.LoadEmployeeData())
                                                       .ContinueWith(antecedent => antecedent.Result);

            return await Task.FromResult(getEmployeeTask.Result);
        }

        public static async Task<List<Department>> LoadDepartmentDataAsync()
        {
            string fileName = "/home/bthomas/Projects/NetCore/REA.Accounting/test/UnitTests/Data/Departments.json";
            string jsonString = File.ReadAllText(fileName);

            return await Task.FromResult(JsonConvert.DeserializeObject<List<Department>>(jsonString)!);
        }

        public static async Task<List<Shift>> LoadShiftDataAsync()
        {
            string fileName = "/home/bthomas/Projects/NetCore/REA.Accounting/test/UnitTests/Data/Shifts.json";
            string jsonString = File.ReadAllText(fileName);

            return await Task.FromResult(JsonConvert.DeserializeObject<List<Shift>>(jsonString)!);
        }

        public static async Task<List<Address>> LoadAddressData()
        {
            string fileName = "/home/bthomas/Projects/NetCore/REA.Accounting/test/UnitTests/Data/Address-SM.json";
            string jsonString = File.ReadAllText(fileName);

            return await Task.FromResult(JsonConvert.DeserializeObject<List<Address>>(jsonString)!);
        }

        public static async Task<List<EmailAddress>> LoadEmailAddressData()
        {
            string fileName = "/home/bthomas/Projects/NetCore/REA.Accounting/test/UnitTests/Data/EmailAddress-SM.json";
            string jsonString = File.ReadAllText(fileName);

            return await Task.FromResult(JsonConvert.DeserializeObject<List<EmailAddress>>(jsonString)!);
        }

        public static async Task<List<EmployeeDepartmentHistory>> LoadDepartmentHistoryData()
        {
            string fileName = "/home/bthomas/Projects/NetCore/REA.Accounting/test/UnitTests/Data/EmployeeDepartmentHistory-SM.json";
            string jsonString = File.ReadAllText(fileName);

            return await Task.FromResult(JsonConvert.DeserializeObject<List<EmployeeDepartmentHistory>>(jsonString)!);
        }

        public static async Task<List<EmployeePayHistory>> LoadPayHistoryData()
        {
            string fileName = "/home/bthomas/Projects/NetCore/REA.Accounting/test/UnitTests/Data/EmployeePayHistory-SM.json";
            string jsonString = File.ReadAllText(fileName);

            return await Task.FromResult(JsonConvert.DeserializeObject<List<EmployeePayHistory>>(jsonString)!);
        }


        public static async Task<List<Person>> LoadPersonData()
        {
            string fileName = "/home/bthomas/Projects/NetCore/REA.Accounting/test/UnitTests/Data/Person-SM.json";
            string jsonString = File.ReadAllText(fileName);

            return await Task.FromResult(JsonConvert.DeserializeObject<List<Person>>(jsonString)!);
        }

        public static async Task<List<PersonPhone>> LoadTelephoneDataAsync()
        {
            const string fileName = "/home/bthomas/Projects/NetCore/REA.Accounting/test/UnitTests/Data/Telephone-SM.json";
            string jsonString = File.ReadAllText(fileName);

            return await Task.FromResult(JsonConvert.DeserializeObject<List<PersonPhone>>(jsonString)!);
        }

        public static List<PersonPhone> LoadTelephoneData()
        {
            const string fileName = "/home/bthomas/Projects/NetCore/REA.Accounting/test/UnitTests/Data/Telephone-SM.json";
            string jsonString = File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<List<PersonPhone>>(jsonString)!;
        }

        private static HashSet<Employee> LoadEmployeeData()
        {
            const string fileName = "/home/bthomas/Projects/NetCore/REA.Accounting/test/UnitTests/Data/Employees-SM.json";
            string jsonString = File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<HashSet<Employee>>(jsonString)!;
        }

        private static HashSet<BusinessEntity> LoadBusinessEntityData()
        {
            string fileName = $"{BaseFilePath}BusinessEntity-XS.json";
            string jsonString = File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<HashSet<BusinessEntity>>(jsonString)!;
        }

    }
}