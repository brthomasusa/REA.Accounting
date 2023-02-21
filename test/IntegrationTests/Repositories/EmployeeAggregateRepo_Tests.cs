using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.Shared;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.IntegrationTests.Base;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Repositories;

namespace REA.Accounting.IntegrationTests.Repositories
{
    public class EmployeeAggregateRepo_Tests : TestBase
    {
        private readonly IWriteRepositoryManager _writeRepository;

        public EmployeeAggregateRepo_Tests()
            => _writeRepository = new WriteRepositoryManager(_dbContext);

        [Fact]
        public async Task GetById_EmployeeAggregateRepo_ShouldSucceed()
        {
            OperationResult<Employee> result = await _writeRepository.EmployeeAggregate.GetByIdAsync(2);

            Assert.True(result.Success);

            Address address = result.Result.Addresses.ToList()[0];
            Assert.Equal("7559 Worth Ct.", address.AddressLine1);
        }

        [Fact]
        public async Task InsertAsync_EmployeeAggregateRepo_ShouldSucceed()
        {
            Employee employee = GetEmployeeForCreate();

            OperationResult<int> result = await _writeRepository.EmployeeAggregate.InsertAsync(employee);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task Update_EmployeeAggregateRepo_ShouldSucceed()
        {
            OperationResult<Employee> getResult = await _writeRepository.EmployeeAggregate.GetByIdAsync(16);

            Assert.True(getResult.Success);

            OperationResult<Employee> updateResult =
                getResult.Result.Update("EM", NameStyleEnum.Western, "Mr.", "Jabu", "Jabi", "J", "Sr.",
                                        EmailPromotionEnum.None, "98765432", @"adventure-works\jabi", "Big Dog",
                                        new DateOnly(2000, 1, 31), "M", "M", new DateOnly(2018, 5, 4), true, 5, 1, true);

            Assert.True(updateResult.Success);

            OperationResult<int> saveResult = await _writeRepository.EmployeeAggregate.Update(updateResult.Result);

            Assert.True(saveResult.Success);

            getResult = await _writeRepository.EmployeeAggregate.GetByIdAsync(16);
            Assert.Equal(@"adventure-works\jabi", getResult.Result.LoginID);
        }

        [Fact]
        public async Task Delete_Employee_EmployeeAggregateRepo_ShouldSucceed()
        {
            OperationResult<Employee> getResult = await _writeRepository.EmployeeAggregate.GetByIdAsync(16);

            Assert.True(getResult.Success);

            OperationResult<int> deleteResult = await _writeRepository.EmployeeAggregate.Delete(getResult.Result);

            Assert.True(deleteResult.Success);

            OperationResult<Employee> test = await _writeRepository.EmployeeAggregate.GetByIdAsync(16);
            Assert.Null(test.Result);
        }

        [Fact]
        public async Task ValidatePersonNameIsUnique_EmployeeAggregateRepo_NewRecord_ShouldReturnTrue()
        {
            Result result = await _writeRepository.EmployeeAggregate.ValidatePersonNameIsUnique(0, "Henry", "Jones", "Z");

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ValidatePersonNameIsUnique_EmployeeAggregateRepo_EditingExistingWithoutNameChange_ShouldReturnTrue()
        {
            Result result = await _writeRepository.EmployeeAggregate.ValidatePersonNameIsUnique(25, "James", "Hamilton", "R");

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ValidatePersonNameIsUnique_EmployeeAggregateRepo_EditingExistingNameChangeWouldCauseDupe_ShouldReturnFalse()
        {
            // EmployeeID 2 is Terri Lee Duffy, changing name to James R Hamilton would be a duplicate name of EmployeeID 25
            Result result = await _writeRepository.EmployeeAggregate.ValidatePersonNameIsUnique(2, "James", "Hamilton", "R");

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task ValidateNationalIdNumberIsUnique_EmployeeAggregateRepo_NewRecord_ShouldReturnTrue()
        {
            Result result = await _writeRepository.EmployeeAggregate.ValidateNationalIdNumberIsUnique(0, "632145877");

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ValidateNationalIdNumberIsUnique_EmployeeAggregateRepo_EditingExistingWithoutNatlIDChange_ShouldReturnTrue()
        {
            Result result = await _writeRepository.EmployeeAggregate.ValidateNationalIdNumberIsUnique(2, "245797967");

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ValidateNationalIdNumberIsUnique_EmployeeAggregateRepo_EditingExistingWithNatlIDChange_ShouldReturnFalse()
        {
            Result result = await _writeRepository.EmployeeAggregate.ValidateNationalIdNumberIsUnique(1, "245797967");

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task ValidateEmployeeEmailIsUnique_EmployeeAggregateRepo_NewRecord_ShouldReturnTrue()
        {
            Result result = await _writeRepository.EmployeeAggregate.ValidateEmployeeEmailIsUnique(0, "david4@adventure-works.com");

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ValidateEmployeeEmailIsUnique_EmployeeAggregateRepo_EditingExistingWithoutEmailChange_ShouldReturnTrue()
        {
            Result result = await _writeRepository.EmployeeAggregate.ValidateEmployeeEmailIsUnique(16, "david0@adventure-works.com");

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ValidateEmployeeEmailIsUnique_EmployeeAggregateRepo_EditingExistingWithEmailChange_ShouldReturnFalse()
        {
            Result result = await _writeRepository.EmployeeAggregate.ValidateEmployeeEmailIsUnique(25, "david0@adventure-works.com");

            Assert.True(result.IsFailure);
        }

        private static Employee GetEmployeeForCreate()
            => Employee.Create
                (
                    0,
                    "EM",
                    Core.Shared.NameStyleEnum.Western,
                    "Mr",
                    "John",
                    "Doe",
                    "D",
                    "Senior",
                    "358987145",
                    "adventure-works\\john10",
                    "Tool Designer",
                    new DateOnly(1990, 2, 21),
                    "M",
                    "M",
                    new DateOnly(2023, 1, 5),
                    true,
                    0,
                    0,
                    true
                );
    }
}