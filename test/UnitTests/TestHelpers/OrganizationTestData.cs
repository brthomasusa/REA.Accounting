using REA.Accounting.Core.Organization;
using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.CommonValueObjects;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.UnitTests.TestHelpers
{
    public static class OrganizationTestData
    {
        public static Company GetCompanyWithValidData()
        {
            Result<Company> result = Company.Create
            (
                0,
                "Test Company",
                "Test Company",
                "123456789",
                "https://www.testcompany.com",
                "123 Main Street",
                "Suite 10",
                "Austin",
                73,
                "78123",
                "123 Main Street",
                "Suite 10",
                "Austin",
                73,
                "78123",
                "512-555-5555",
                "512-555-9999"
            );

            return result.Value;
        }

        public static Company GetCompanyWithDeptAndShift()
        {
            Result<Company> result = Company.Create
            (
                0,
                "Test Company",
                "Test Company",
                "123456789",
                "https://www.testcompany.com",
                "123 Main Street",
                "Suite 10",
                "Austin",
                73,
                "78123",
                "123 Main Street",
                "Suite 10",
                "Austin",
                73,
                "78123",
                "512-555-5555",
                "512-555-9999"
            );

            _ = result.Value.AddDepartment
            (
                1,
                "QA",
                "Quality Assurance"
            );

            _ = result.Value.AddDepartment
            (
                2,
                "R&D",
                "Research and Development"
            );

            _ = result.Value.AddShift
            (
                1,
                "Midnight",
                23,
                0,
                7,
                0
            );

            _ = result.Value.AddShift
            (
                2,
                "Weekend",
                10,
                0,
                18,
                0
            );

            return result.Value;
        }

    }
}
/*
            int companyID,
            string companyName,
            string legalName,
            string ein,
            string companyWebSite,
            string mailLine1,
            string? mailLine2,
            string mailCity,
            int mailStateProvinceID,
            string mailPostalCode,
            string deliveryLine1,
            string? deliveryLine2,
            string deliveryCity,
            int deliveryStateProvinceID,
            string deliveryPostalCode,
            string telephone,
            string fax
*/