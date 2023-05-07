namespace REA.Accounting.IntegrationTests.DbContext
{
    public class DbContextRetrieve_Tests : TestBase
    {
        [Fact]
        public void Get_ContactTypes_ShouldSucceed()
        {
            //SETUP

            //ATTEMPT
            var contactTypes = _dbContext.ContactType!.ToList();
            int count = contactTypes.Count;

            //VERIFY
            Assert.Equal(20, count);
        }

        [Fact]
        public void Get_AddressTypes_ShouldSucceed()
        {
            //SETUP

            //ATTEMPT
            var addressTypes = _dbContext.AddressType!.ToList();
            int count = addressTypes.Count;

            //VERIFY
            Assert.Equal(6, count);
        }

        [Fact]
        public void Get_PhoneNumberTypes_ShouldSucceed()
        {
            //SETUP

            //ATTEMPT
            var PhoneNumberTypes = _dbContext.PhoneNumberType!.ToList();
            int count = PhoneNumberTypes.Count;

            //VERIFY
            Assert.Equal(3, count);
        }

        [Fact]
        public void Get_CountryRegions_ShouldSucceed()
        {
            //SETUP

            //ATTEMPT
            var regions = _dbContext.CountryRegion!.ToList();
            int count = regions.Count;

            //VERIFY
            Assert.Equal(238, count);
        }

        [Fact]
        public void Get_SalesTerritory_ShouldSucceed()
        {
            //SETUP

            //ATTEMPT
            var regions = _dbContext.SalesTerritory!.ToList();
            int count = regions.Count;

            //VERIFY
            Assert.Equal(10, count);
        }

        [Fact]
        public void Get_StateProvince_ShouldSucceed()
        {
            //SETUP

            //ATTEMPT
            var regions = _dbContext.StateProvince!.ToList();
            int count = regions.Count;

            //VERIFY
            Assert.Equal(181, count);
        }

        [Fact]
        public void DbContext_Person_HR_ShouldSucceed()
        {
            //SETUP

            //ATTEMPT
            var company = _dbContext.Company!.ToList();
            var businessEntities = _dbContext.BusinessEntity!.ToList();
            var people = _dbContext.Person!.ToList();
            var addresses = _dbContext.Address!.ToList();
            var businessEntityAddresses = _dbContext.BusinessEntityAddress!.ToList();
            var emailAddresses = _dbContext.EmailAddress!.ToList();
            var telephones = _dbContext.PersonPhone!.ToList();
            var departments = _dbContext.Department!.ToList();
            var shifts = _dbContext.Shift!.ToList();
            var employees = _dbContext.Employee!.ToList();
            var departmentHistories = _dbContext.EmployeeDepartmentHistory!.ToList();
            var payHistories = _dbContext.EmployeePayHistory!.ToList();

            int companyCount = company.Count;
            int businessEntityCount = businessEntities.Count;
            int peopleCount = people.Count;
            int addressCount = addresses.Count;
            int businessEntityAddressCount = businessEntityAddresses.Count;
            int emailAddressCount = emailAddresses.Count;
            int telephoneCount = telephones.Count;
            int departmentCount = departments.Count;
            int shiftCount = shifts.Count;
            int employeeCount = employees.Count;
            int departmentHistoryCount = departmentHistories.Count;
            int payHistoryCount = payHistories.Count;

            //VERIFY
            Assert.Equal(1, companyCount);
            Assert.True(businessEntityCount > 0);
            Assert.True(peopleCount > 0);
            Assert.True(addressCount > 0);
            Assert.True(businessEntityAddressCount > 0);
            Assert.True(emailAddressCount > 0);
            Assert.True(telephoneCount > 0);
            Assert.True(departmentCount > 0);
            Assert.True(shiftCount > 0);
            Assert.True(employeeCount > 0);
            Assert.True(departmentHistoryCount > 0);
            Assert.True(payHistoryCount > 0);
        }


    }
}