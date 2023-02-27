using TestSupport.EfHelpers;
using Microsoft.Extensions.Logging.Abstractions;

using REA.Accounting.Application.HumanResources.CreateEmployee;
using REA.Accounting.Application.HumanResources.DeleteEmployee;
using REA.Accounting.Application.HumanResources.UpdateEmployee;
using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.UnitTests.TestHelpers;

namespace REA.Accounting.UnitTests.CommandHandlers
{
    public class HrCommandHandler_Tests : IDisposable
    {
        private EfCoreContext? _context;
        private readonly IWriteRepositoryManager _writeRepository;

        public HrCommandHandler_Tests()
        {
            ConfigureDbContextAsync();
            _writeRepository = new WriteRepositoryManager(_context!, new NullLogger<WriteRepositoryManager>());
        }

        public void Dispose()
        {
            _context!.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task Handle_CreateEmployeeCommandHandler_ShouldSucceed()
        {
            CreateEmployeeCommand command = GetCreateEmployeeCommand();
            CreateEmployeeCommandHandler handler = new(_writeRepository);

            Result<int> result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsSuccess);
            Assert.True(result.Value > 0);
        }

        [Fact]
        public async Task Handle_UpdateEmployeeCommandHandler_ShouldSucceed()
        {
            UpdateEmployeeCommand command = GetUpdateEmployeeCommand();
            UpdateEmployeeCommandHandler handler = new(_writeRepository);

            Result<int> result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_DeleteEmployeeCommandHandler_ShouldSucceed()
        {
            DeleteEmployeeCommand command = new(EmployeeID: 273);
            DeleteEmployeeCommandHandler handler = new(_writeRepository);

            Result<int> result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsSuccess);
        }

        private static CreateEmployeeCommand GetCreateEmployeeCommand()
            => new(
                EmployeeID: 0,
                PersonType: "EM",
                NameStyle: false,
                Title: "Mr.",
                FirstName: "Johnny",
                LastName: "Doe",
                MiddleName: "J",
                Suffix: null,
                EmailPromotion: 2,
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

        private static UpdateEmployeeCommand GetUpdateEmployeeCommand()
            => new
            (
                EmployeeID: 273,
                PersonType: "EM",
                NameStyle: false,
                Title: "Mr.",
                FirstName: "Johnny",
                LastName: "Doe",
                MiddleName: "J",
                Suffix: null,
                EmailPromotion: 2,
                NationalID: "112432117",
                Login: @"adventure-works\johnny0",
                JobTitle: "The Man",
                BirthDate: new DateTime(2000, 1, 28),
                MaritalStatus: "M",
                Gender: "M",
                HireDate: new DateTime(2020, 1, 28),
                Salaried: true,
                Vacation: 5,
                SickLeave: 1,
                Active: true
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