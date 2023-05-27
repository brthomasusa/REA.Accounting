#pragma warning disable CS8600, CS8625

using REA.Accounting.Core.Organization;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.UnitTests.TestHelpers;

namespace REA.Accounting.UnitTests.HumanResources
{
    public class OrganizationAggregate_Tests
    {
        [Fact]
        public void Company_Create_ValidData_ReturnSuccess()
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

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Company_Update_ValidData_ReturnSuccess()
        {
            Company company = OrganizationTestData.GetCompanyWithDeptAndShift();

            Result<Company> result = company.Update
            (
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

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Company_CreateDept_DuplicateID_ReturnFailure()
        {
            Company company = OrganizationTestData.GetCompanyWithDeptAndShift();

            Result result = company.AddDepartment
            (
                1,
                "Q&A",
                "Quality Assurance"
            );

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Company_CreateDept_DuplicateDeptName_ReturnFailure()
        {
            Company company = OrganizationTestData.GetCompanyWithDeptAndShift();

            Result result = company.AddDepartment
            (
                3,
                "QA",
                "Quality Assurance"
            );

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Company_UpdateDept_ValidData_ReturnSuccess()
        {
            Company company = OrganizationTestData.GetCompanyWithDeptAndShift();

            Result result = company.UpdateDepartment
            (
                1,
                "Q&A",
                "Quality Assurance"
            );

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Company_UpdateDept_InvalidDeptID_ReturnFailure()
        {
            Company company = OrganizationTestData.GetCompanyWithDeptAndShift();

            Result result = company.UpdateDepartment
            (
                3,
                "Q&A",
                "Quality Assurance"
            );

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Company_UpdateDept_DuplicateDeptName_ReturnFailure()
        {
            Company company = OrganizationTestData.GetCompanyWithDeptAndShift();

            Result result = company.UpdateDepartment
            (
                2,
                "QA",
                "Quality Assurance"
            );

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Company_DeleteDepartment_Valid_ReturnSuccess()
        {
            Company company = OrganizationTestData.GetCompanyWithDeptAndShift();

            Result result = company.DeleteDepartment(1);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Company_DeleteDepartment_InvalidID_ReturnFailure()
        {
            Company company = OrganizationTestData.GetCompanyWithDeptAndShift();

            Result result = company.DeleteDepartment(10);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Shift_Create_ShouldSucceed()
        {
            Company company = OrganizationTestData.GetCompanyWithDeptAndShift();
            const string name = "Day Shift";

            Result result = company.AddShift
            (
                3,
                name,
                8,
                0,
                17,
                0
            );

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Shift_Create_DupeID_ShouldFail()
        {
            Company company = OrganizationTestData.GetCompanyWithDeptAndShift();
            const string name = "Day Shift";

            Result result = company.AddShift
            (
                1,
                name,
                8,
                0,
                17,
                0
            );

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Shift_Create_DupeHours_ShouldFail()
        {
            Company company = OrganizationTestData.GetCompanyWithDeptAndShift();
            const string name = "Day Shift";

            Result result = company.AddShift
            (
                3,
                name,
                23,
                0,
                7,
                0
            );

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Shift_Create_InvalidStartHour_ShouldFail()
        {
            Company company = OrganizationTestData.GetCompanyWithDeptAndShift();
            const string name = "Day Shift";

            Result result = company.AddShift
            (
                3,
                name,
                24,
                0,
                7,
                0
            );

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Shift_Update_ValidData_ShouldSucceed()
        {
            Company company = OrganizationTestData.GetCompanyWithDeptAndShift();
            const string name = "Evening Shift";

            Result result = company.UpdateShift
            (
                1,
                name,
                15,
                0,
                23,
                0
            );

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Shift_Update_InvalidID_ShouldFail()
        {
            Company company = OrganizationTestData.GetCompanyWithDeptAndShift();
            const string name = "Evening Shift";

            Result result = company.UpdateShift
            (
                11,
                name,
                15,
                1,
                23,
                1
            );

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Shift_Update_DupeName_ShouldFail()
        {
            Company company = OrganizationTestData.GetCompanyWithDeptAndShift();
            const string name = "Weekend";

            Result result = company.UpdateShift
            (
                1,
                name,
                15,
                0,
                23,
                0
            );

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Shift_Update_DupeHours_ShouldFail()
        {
            Company company = OrganizationTestData.GetCompanyWithDeptAndShift();
            const string name = "Midnight";

            Result result = company.UpdateShift
            (
                1,
                name,
                10,
                0,
                18,
                0
            );

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Shift_Delete_ValidData_ShouldSucceed()
        {
            Company company = OrganizationTestData.GetCompanyWithDeptAndShift();

            Result result = company.DeleteShift(1);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Shift_Delete_InvalidID_ShouldFail()
        {
            Company company = OrganizationTestData.GetCompanyWithDeptAndShift();

            Result result = company.DeleteShift(11);

            Assert.True(result.IsFailure);
        }
    }
}