using TestSupport.EfHelpers;

using REA.Accounting.Application.HumanResources.Commands.CreateEmployee;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.Interfaces;
using REA.Accounting.Core.Shared;
using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.Repositories.HumanResources;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.UnitTests.TestHelpers;

namespace REA.Accounting.UnitTests.CommandHandlers
{
    public class HrCommandHandler_Tests : IDisposable
    {
        private EfCoreContext? _context;

        public HrCommandHandler_Tests()
            => ConfigureDbContextAsync();

        public void Dispose()
        {
            _context!.Dispose();
        }

        [Fact]
        public async Task Handle_CreateEmployeeCommandHandler_ShouldSucceed()
        {
            IEmployeeAggregateRepository repo = new EmployeeAggregateRepo(_context!);
            CreateEmployeeCommand command = GetCreateEmployeeCommand();
            CreateEmployeeCommandHandler handler = new(repo);

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
                BirthDate: new DateOnly(2000, 1, 28),
                MaritalStatus: "M",
                Gender: "M",
                HireDate: new DateOnly(2020, 1, 28),
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

        private async void ConfigureDbContextAsync()
        {
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();
            _context = new EfCoreContext(options);
            _context.Database.EnsureCreated();
            await _context.SeedLookupData();
            await _context.SeedPersonAndHrData();
        }
    }
}