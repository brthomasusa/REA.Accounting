using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.Interfaces;
using REA.Accounting.Core.Shared;
using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.Infrastructure.Persistence.Specifications;
using REA.Accounting.Infrastructure.Persistence.Specifications.Person;
using REA.Accounting.SharedKernel;
using REA.Accounting.SharedKernel.CommonValueObjects;
using REA.Accounting.SharedKernel.Utilities;
using DataModelEmployee = REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources.Employee;
using DomainModelEmployee = REA.Accounting.Core.HumanResources.Employee;


namespace REA.Accounting.UnitTests.Repositories
{
    public class EmployeeAggregateRepo : IEmployeeAggregateRepository
    {
        private EfCoreContext _context;
        private IUnitOfWork _unitOfWork;

        public EmployeeAggregateRepo(EfCoreContext ctx)
        {
            _context = ctx;
            _unitOfWork = new UnitOfWork(_context);
        }

        public IUnitOfWork UnitOfWork => new UnitOfWork(_context);

        public async Task<OperationResult<DomainModelEmployee>> GetByIdAsync(int empployeeID, bool asNoTracking = false)
        {
            try
            {
                CancellationToken cancellationToken = default;

                var person = await
                    SpecificationEvaluator.Default.GetQuery
                    (
                        asNoTracking ? _context.Set<PersonModel>().AsNoTracking() : _context.Set<PersonModel>(),
                        new PersonByIDWithEmployeeSpec(empployeeID)
                    ).FirstOrDefaultAsync(cancellationToken);

                // Create employee domain object from person data model
                DomainModelEmployee employee = CreateDomainEmployee(ref person!);

                // Add addresses to employee from person data model
                if (person!.Addresses.ToList().Any())
                {
                    person!.Addresses.ToList().ForEach(addr =>
                        employee.AddAddress(addr.AddressID,
                                            addr.BusinessEntityID,
                                            (AddressTypeEnum)addr.AddressTypeID,
                                            addr.Address!.AddressLine1!,
                                            addr.Address.AddressLine2,
                                            addr.Address!.City!,
                                            addr.Address.StateProvinceID,
                                            addr.Address!.PostalCode!));
                }


                // Add email addresses to employee from person data model
                if (person.EmailAddresses.ToList().Any())
                {
                    person.EmailAddresses.ToList().ForEach(email =>
                        employee.AddEmailAddress(
                            email.BusinessEntityID,
                            email.EmailAddressID,
                            email.MailAddress!
                        ));
                }

                // Add email addresses to employee from person data model
                if (person!.Telephones.ToList().Any())
                {
                    person!.Telephones.ToList().ForEach(tel =>
                        employee.AddPhoneNumbers(
                            tel.BusinessEntityID,
                            (PhoneNumberTypeEnum)tel.PhoneNumberTypeID,
                            tel.PhoneNumber!
                        ));
                }

                return OperationResult<DomainModelEmployee>.CreateSuccessResult(employee);
            }
            catch (Exception ex)
            {
                return OperationResult<DomainModelEmployee>.CreateFailure(ex.Message);
            }
        }

        public async Task<OperationResult<int>> InsertAsync(DomainModelEmployee employee)
        {
            try
            {
                BusinessEntity entity = new()
                {
                    BusinessEntityID = 0,
                    PersonModel = new()
                    {
                        PersonType = employee.PersonType,
                        NameStyle = (int)employee.NameStyle,
                        Title = employee.Title,
                        FirstName = employee.FirstName,
                        MiddleName = employee.MiddleName,
                        LastName = employee.LastName,
                        Suffix = employee.Suffix,
                        EmailPromotion = (int)employee.EmailPromotions,
                        Employee = new DataModelEmployee()
                        {
                            NationalIDNumber = employee.NationalIDNumber,
                            LoginID = employee.LoginID,
                            JobTitle = employee.JobTitle,
                            BirthDate = employee.BirthDate.ToDateTime(new TimeOnly()),
                            MaritalStatus = employee.MaritalStatus,
                            Gender = employee.Gender,
                            HireDate = employee.HireDate.ToDateTime(new TimeOnly()),
                            SalariedFlag = employee.IsSalaried,
                            VacationHours = employee.VacationHours,
                            SickLeaveHours = employee.SickLeaveHours,
                            CurrentFlag = employee.IsActive
                        }
                    }
                };

                await _context.AddAsync(entity);
                await _unitOfWork.CommitAsync();

                return OperationResult<int>.CreateSuccessResult(entity.BusinessEntityID);

            }
            catch (Exception ex)
            {
                return OperationResult<int>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }

            throw new NotImplementedException();
        }

        public Task<OperationResult<bool>> Delete(DomainModelEmployee entity)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<bool>> Remove(IEnumerable<DomainModelEmployee> entitiesToRemove)
        {
            throw new NotImplementedException();
        }

        private DomainModelEmployee CreateDomainEmployee(ref PersonModel person)
        {
            DomainModelEmployee domainObj = DomainModelEmployee.Create
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

            // Add dept histories to employee from person data model
            person!.Employee!.DepartmentHistories.ToList().ForEach(dept =>
                domainObj.AddDepartmentHistory(dept.BusinessEntityID,
                                               dept.ShiftID,
                                               DateOnly.FromDateTime(dept.StartDate),
                                               dept.EndDate));

            // Add pay histories to employee from person data model
            person!.Employee!.PayHistories.ToList().ForEach(pay =>
                domainObj.AddPayHistory(
                    pay.BusinessEntityID,
                    pay.RateChangeDate,
                    pay.Rate,
                    (PayFrequencyEnum)pay.PayFrequency
                ));

            return domainObj;
        }
    }
}