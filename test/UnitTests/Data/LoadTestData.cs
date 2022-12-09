using System.Text;
using System.Text.Json;
using REA.Accounting.Core.HumanResources;

namespace REA.Accounting.UnitTests.Data
{
    public static class LoadTestData
    {
        public static async Task<List<Employee>> LoadEmployeeData()
        {
            string fileName = "/home/bthomas/Projects/NetCore/REA.Accounting/test/UnitTests/Data/Employees.json";

            using FileStream openStream = File.OpenRead(fileName);
            List<Employee>? employees = await JsonSerializer.DeserializeAsync<List<Employee>>(openStream);

            return employees!;
        }

        public static async Task<List<Department>> LoadDepartmentData()
        {
            string fileName = "/home/bthomas/Projects/NetCore/REA.Accounting/test/UnitTests/Data/Departments.json";

            using FileStream openStream = File.OpenRead(fileName);
            List<Department>? departments = await JsonSerializer.DeserializeAsync<List<Department>>(openStream);

            return departments!;
        }
    }
}