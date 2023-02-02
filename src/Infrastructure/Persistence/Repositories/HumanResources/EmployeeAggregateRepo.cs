using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.Interfaces;
using REA.Accounting.Core.Shared;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.Specifications.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Specifications.Person;
using REA.Accounting.SharedKernel.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

using EmployeeDataModel = REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources.Employee;
using DomainModelEmployee = REA.Accounting.Core.HumanResources.Employee;

namespace REA.Accounting.Infrastructure.Persistence.Repositories.HumanResources
{
    public sealed class EmployeeAggregateRepository : IEmployeeAggregateRepository
    {
        private EfCoreContext _context;
        private IUnitOfWork _unitOfWork;

        public EmployeeAggregateRepository(EfCoreContext ctx)
        {
            _context = ctx;
            _unitOfWork = new UnitOfWork(_context);
        }

        public async Task<OperationResult<bool>> ValidatePersonNameIsUnique(string fname, string lname, string? middleName, bool asNoTracking = true)
        {
            try
            {
                CancellationToken cancellationToken = default;

                var person = await
                    SpecificationEvaluator.Default.GetQuery
                    (
                        asNoTracking ? _context.Set<PersonDataModel>().AsNoTracking() : _context.Set<PersonDataModel>(),
                        new ValidateEmployeeNameIsUniqueSpec(fname, lname, middleName)
                    ).FirstOrDefaultAsync(cancellationToken);

                return OperationResult<bool>.CreateSuccessResult(person is null ? true : false);

            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }

        public async Task<OperationResult<bool>> ValidateNationalIdNumberIsUnique(string nationalIdNumber, bool asNoTracking = true)
        {
            try
            {
                CancellationToken cancellationToken = default;

                var employee = await
                    SpecificationEvaluator.Default.GetQuery
                    (
                        asNoTracking ? _context.Set<EmployeeDataModel>().AsNoTracking() : _context.Set<EmployeeDataModel>(),
                        new ValidateNationalIdNumberIsUniqueSpec("295847004")
                    ).FirstOrDefaultAsync(cancellationToken);

                return OperationResult<bool>.CreateSuccessResult(employee is null ? true : false);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }


        public async Task<OperationResult<bool>> ValidateEmployeeEmailIsUnique(string emailAddres, bool asNoTracking = true)
        {
            try
            {
                CancellationToken cancellationToken = default;

                var person = await
                    SpecificationEvaluator.Default.GetQuery
                    (
                        asNoTracking ? _context.Set<PersonDataModel>().AsNoTracking() : _context.Set<PersonDataModel>(),
                        new ValidateEmployeeEmailIsUniqueSpec(emailAddres)
                    ).FirstOrDefaultAsync(cancellationToken);

                return OperationResult<bool>.CreateSuccessResult(person is null ? true : false);

            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }


        public async Task<OperationResult<DomainModelEmployee>> GetEmployeeOnlyAsync(int empployeeID, bool asNoTracking = false)
        {
            try
            {
                CancellationToken cancellationToken = default;

                var person = await
                    SpecificationEvaluator.Default.GetQuery
                    (
                        asNoTracking ? _context.Set<PersonDataModel>().AsNoTracking() : _context.Set<PersonDataModel>(),
                        new PersonByIDWithEmployeeOnlySpec(empployeeID)
                    ).FirstOrDefaultAsync(cancellationToken);

                // Create employee domain object from person data model
                DomainModelEmployee employee = CreateDomainEmployee(ref person!);

                return OperationResult<DomainModelEmployee>.CreateSuccessResult(employee);
            }
            catch (Exception ex)
            {
                return OperationResult<DomainModelEmployee>.CreateFailure(ex.Message);
            }
        }

        public async Task<OperationResult<DomainModelEmployee>> GetByIdAsync(int empployeeID, bool asNoTracking = false)
        {
            try
            {
                CancellationToken cancellationToken = default;

                var person = await
                    SpecificationEvaluator.Default.GetQuery
                    (
                        asNoTracking ? _context.Set<PersonDataModel>().AsNoTracking() : _context.Set<PersonDataModel>(),
                        new PersonByIDWithEmployeeSpec(empployeeID)
                    ).FirstOrDefaultAsync(cancellationToken);

                // Create employee domain object from person data model
                DomainModelEmployee employee = CreateDomainEmployee(ref person!);

                // Add addresses to employee from person data model
                if (person!.BusinessEntityAddresses.ToList().Any())
                {
                    person!.BusinessEntityAddresses.ToList().ForEach(addr =>
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
                        NameStyle = employee.NameStyle == NameStyleEnum.Western ? false : true,
                        Title = employee.Title,
                        FirstName = employee.FirstName,
                        MiddleName = employee.MiddleName,
                        LastName = employee.LastName,
                        Suffix = employee.Suffix,
                        EmailPromotion = (int)employee.EmailPromotions,
                        Employee = new EmployeeDataModel()
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
        }

        public async Task<OperationResult<bool>> Update(DomainModelEmployee entity)
        {
            try
            {
                CancellationToken cancellationToken = default;

                var person = await
                    SpecificationEvaluator.Default.GetQuery
                    (
                        _context.Set<PersonDataModel>(),
                        new PersonByIDWithEmployeeOnlySpec(entity.Id)
                    ).FirstOrDefaultAsync(cancellationToken);

                if (person is not null)
                {
                    person.PersonType = entity.PersonType;
                    person.NameStyle = entity.NameStyle == NameStyleEnum.Western ? false : true;
                    person.Title = entity.Title;
                    person.FirstName = entity.FirstName;
                    person.MiddleName = entity.MiddleName!;
                    person.LastName = entity.LastName;
                    person.Suffix = entity.Suffix;
                    person.EmailPromotion = (int)entity.EmailPromotions;

                    person.Employee!.NationalIDNumber = entity.NationalIDNumber;
                    person.Employee!.LoginID = entity.LoginID;
                    person.Employee!.JobTitle = entity.JobTitle;
                    person.Employee!.BirthDate = entity.BirthDate.ToDateTime(new TimeOnly());
                    person.Employee!.MaritalStatus = entity.MaritalStatus;
                    person.Employee!.Gender = entity.Gender;
                    person.Employee!.HireDate = entity.HireDate.ToDateTime(new TimeOnly());
                    person.Employee!.SalariedFlag = entity.IsSalaried;
                    person.Employee!.VacationHours = entity.VacationHours;
                    person.Employee!.SickLeaveHours = entity.SickLeaveHours;
                    person.Employee!.CurrentFlag = entity.IsActive;

                    await _unitOfWork.CommitAsync();

                    return OperationResult<bool>.CreateSuccessResult(true);
                }
                else
                {
                    return OperationResult<bool>.CreateFailure("Failed to retrieve employee for editing.");
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }

        public async Task<OperationResult<bool>> Delete(DomainModelEmployee entity)
        {
            try
            {
                EmployeeDataModel? employee = await _context.Employee!.FindAsync(entity.Id);
                PersonDataModel? person = await _context.Person!.FindAsync(entity.Id);
                BusinessEntity? businessEntity = await _context.BusinessEntity!.FindAsync(entity.Id);

                if (employee is not null && person is not null && businessEntity is not null)
                {
                    RemovePayHistories(entity.Id);
                    RemoveDepartmentHistories(entity.Id);
                    RemovePersonPhones(entity.Id);
                    RemovePersonEmailAddresses(entity.Id);
                    RemovePersonPasswords(entity.Id);
                    RemoveBusinessEntityAddresses(entity.Id);

                    _context.Employee!.Remove(employee);
                    _context.Person!.Remove(person);
                    _context.BusinessEntity!.Remove(businessEntity);

                    await _unitOfWork.CommitAsync();
                    return OperationResult<bool>.CreateSuccessResult(true);
                }
                else
                {
                    return OperationResult<bool>.CreateFailure("Errors occurred while retrieving employee to be deleted.");
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }

        private void RemovePayHistories(int employeeID)
        {
            if (_context.EmployeePayHistory!.Where(hist => hist.BusinessEntityID == employeeID).Any())
            {
                var histories = _context.EmployeePayHistory!.Where(hist => hist.BusinessEntityID == employeeID).ToList();
                _context.EmployeePayHistory!.RemoveRange(histories);
            }
        }

        private void RemoveDepartmentHistories(int employeeID)
        {
            if (_context.EmployeeDepartmentHistory!.Where(hist => hist.BusinessEntityID == employeeID).Any())
            {
                var histories = _context.EmployeeDepartmentHistory!.Where(hist => hist.BusinessEntityID == employeeID).ToList();
                _context.EmployeeDepartmentHistory!.RemoveRange(histories);
            }
        }

        private void RemovePersonPhones(int employeeID)
        {
            if (_context.PersonPhone!.Where(ph => ph.BusinessEntityID == employeeID).Any())
            {
                var phones = _context.PersonPhone!.Where(ph => ph.BusinessEntityID == employeeID).ToList();
                _context.PersonPhone!.RemoveRange(phones);
            }
        }

        private void RemovePersonPasswords(int employeeID)
        {
            if (_context.Password!.Where(pw => pw.BusinessEntityID == employeeID).Any())
            {
                var passwords = _context.Password!.Where(pw => pw.BusinessEntityID == employeeID).ToList();
                _context.Password!.RemoveRange(passwords);
            }
        }

        private void RemovePersonEmailAddresses(int employeeID)
        {
            if (_context.EmailAddress!.Where(addr => addr.BusinessEntityID == employeeID).Any())
            {
                var addresses = _context.EmailAddress!.Where(addr => addr.BusinessEntityID == employeeID).ToList();
                _context.EmailAddress!.RemoveRange(addresses);
            }
        }

        private void RemoveBusinessEntityAddresses(int employeeID)
        {
            if (_context.BusinessEntityAddress!.Where(addr => addr.BusinessEntityID == employeeID).Any())
            {
                var businessEntityAddresses = _context.BusinessEntityAddress!.Where(addr => addr.BusinessEntityID == employeeID).ToList();
                _context.BusinessEntityAddress!.RemoveRange(businessEntityAddresses);

                RemoveAddresses(employeeID);
            }
        }

        private void RemoveAddresses(int employeeID)
        {
            int[] addressIDs = _context
                .BusinessEntityAddress!
                .Where(addr => addr.BusinessEntityID == employeeID)
                .Select(addr => addr.AddressID)
                .ToArray<int>();

            var addresses = _context.Address!.Where(addr => addressIDs.Contains(addr.AddressID)).ToList();

            if (addresses.Any())
                _context.Address!.RemoveRange(addresses);
        }

        private DomainModelEmployee CreateDomainEmployee(ref PersonDataModel person)
        {
            DomainModelEmployee domainObj = DomainModelEmployee.Create
            (
                person!.BusinessEntityID,
                person!.PersonType!,
                person!.NameStyle ? NameStyleEnum.Eastern : NameStyleEnum.Western,
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