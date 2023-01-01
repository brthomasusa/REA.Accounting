using Microsoft.EntityFrameworkCore;

using REA.Accounting.Core.Interfaces;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.Shared;
using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.Specifications;
using REA.Accounting.Infrastructure.Persistence.Specifications.Person;
using REA.Accounting.SharedKernel.CommonValueObjects;
using REA.Accounting.SharedKernel.Utilities;
using DataModelEmployee = REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources.Employee;
using DomainModelEmployee = REA.Accounting.Core.HumanResources.Employee;

namespace REA.Accounting.UnitTests.Repositories
{
    public class EmployeeAggregateRepo : IEmployeeAggregateRepository
    {
        private EfCoreContext _context;

        public EmployeeAggregateRepo(EfCoreContext ctx) => _context = ctx;

        public async Task<OperationResult<DomainModelEmployee>> GetById(int empployeeID)
        {
            try
            {
                CancellationToken cancellationToken = default;

                var person = await
                    SpecificationEvaluator.GetQuery
                    (
                        _context.Set<PersonModel>(),
                        new PersonByIDWithEmployeeSpecification(empployeeID)
                    ).FirstOrDefaultAsync(cancellationToken);

                DomainModelEmployee employee = DomainModelEmployee.Create
                (
                    person!.BusinessEntityID,
                    person!.PersonType!,
                    (NameStyleEnum)person!.NameStyle,
                    person!.Title!,
                    person!.FirstName!,
                    person!.LastName!,
                    person!.MiddleName!,
                    person!.Suffix!,
                    person!.Employee!.NationalIDNumber!,
                    person!.Employee!.LoginID!,
                    person!.Employee!.JobTitle!,
                    DateOnly.FromDateTime(person!.Employee!.BirthDate),
                    person!.Employee!.MaritalStatus!,
                    person!.Employee!.Gender!,
                    DateOnly.FromDateTime(person!.Employee!.HireDate),
                    person!.Employee!.SalariedFlag,
                    person!.Employee!.VacationHours,
                    person!.Employee!.SickLeaveHours,
                    person!.Employee!.CurrentFlag
                );

                return OperationResult<DomainModelEmployee>.CreateSuccessResult(employee);
            }
            catch (Exception ex)
            {
                return OperationResult<DomainModelEmployee>.CreateFailure(ex.Message);
            }
        }

        public Task<OperationResult<bool>> Create(DomainModelEmployee employee)
        {
            throw new NotImplementedException();
        }
    }
}