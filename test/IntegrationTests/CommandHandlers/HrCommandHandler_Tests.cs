using REA.Accounting.Application.HumanResources.CreateEmployee;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.IntegrationTests.Base;

namespace REA.Accounting.IntegrationTests.CommandHandlers
{
    public class HrCommandHandler_Tests : TestBase
    {
        private IWriteRepositoryManager _writeRepository;

        public HrCommandHandler_Tests()
            => _writeRepository = new WriteRepositoryManager(_dbContext);

        [Fact]
        public async Task Handle_CreateEmployeeCommandHandler_ShouldSucceed()
        {
            CreateEmployeeCommand command = GetCreateEmployeeCommand();
            CreateEmployeeCommandHandler handler = new(_writeRepository);

            int retVal = await handler.Handle(command, new CancellationToken());

            Assert.True(retVal > 0);
        }

        private CreateEmployeeCommand GetCreateEmployeeCommand()
            => new CreateEmployeeCommand
            (
                EmployeeID: 0,
                PersonType: "EM",
                NameStyle: 0,
                Title: "Mr.",
                FirstName: "Johnny",
                LastName: "Doe",
                MiddleName: "J",
                Suffix: null,
                NationalID: "13232145",
                Login: @"adventure-works\johnny0",
                JobTitle: "The Man",
                BirthDate: new DateTime(2000, 1, 28),
                MaritalStatus: "M",
                Gender: "M",
                HireDate: new DateTime(2020, 1, 28),
                Salaried: true,
                Vacation: 5,
                SickLeave: 1,
                Active: true,
                PayRate: 20.00M,
                PayFrequency: 1,
                DepartmentID: 1,
                ShiftID: 1,
                AddressType: 2,
                AddressLine1: "123 street",
                AddressLine2: "Apt 123",
                City: "Somewhere",
                StateCode: 73,
                PostalCode: "12345",
                EmailAddress: "johnny@adventure-works.com",
                PhoneNumber: "555-555-5555",
                PhoneNumberType: 2
            );
    }
}