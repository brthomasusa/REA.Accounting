using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using REA.Accounting.Core.Interfaces;
using REA.Accounting.Core.Shared;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.Mappings.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Specifications.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Specifications.Person;
using REA.Accounting.SharedKernel.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

using EmployeeDataModel = REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources.EmployeeDataModel;
using EmployeeDomainModel = REA.Accounting.Core.HumanResources.Employee;

namespace REA.Accounting.Infrastructure.Persistence.Repositories.HumanResources
{
    public sealed class EmployeeAggregateRepository : IEmployeeAggregateRepository
    {
        private readonly EfCoreContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeAggregateRepository(EfCoreContext ctx)
        {
            _context = ctx;
            _unitOfWork = new UnitOfWork(_context);
        }

        public async Task<Result> ValidatePersonNameIsUnique(int id, string fname, string lname, string? middleName, bool asNoTracking = true)
        {
            try
            {
                CancellationToken cancellationToken = default;

                var person = await
                    SpecificationEvaluator.Default.GetQuery
                    (
                        asNoTracking ? _context.Set<PersonDataModel>().AsNoTracking() : _context.Set<PersonDataModel>(),
                        new ValidateEmployeeNameIsUniqueSpec(fname, lname, middleName)
                    )
                    .Select(s => new { EmployeeID = s.BusinessEntityID, s.FirstName, s.MiddleName, s.LastName })
                    .FirstOrDefaultAsync(cancellationToken);

                if (person is null || person.EmployeeID == id)
                    return Result.Success();

                return Result.Failure
                (
                    new Error
                    (
                        "EmployeeAggregateRepository.ValidatePersonNameIsUnique",
                        $"A person named ${fname} {middleName} {lname} is already in the database."
                    )
                );
            }
            catch (Exception ex)
            {
                return Result.Failure
                (
                    new Error
                    (
                        "EmployeeAggregateRepository.ValidatePersonNameIsUnique",
                        Helpers.GetExceptionMessage(ex)
                    )
                );
            }
        }

        public async Task<Result> ValidateNationalIdNumberIsUnique(int id, string nationalIdNumber, bool asNoTracking = true)
        {
            try
            {
                CancellationToken cancellationToken = default;

                var nationalId = await
                    SpecificationEvaluator.Default.GetQuery
                    (
                        asNoTracking ? _context.Set<EmployeeDataModel>().AsNoTracking() : _context.Set<EmployeeDataModel>(),
                        new ValidateNationalIdNumberIsUniqueSpec(nationalIdNumber)
                    )
                    .Select(s => new { EmployeeID = s.BusinessEntityID, NationalID = s.NationalIDNumber })
                    .FirstOrDefaultAsync(cancellationToken);

                if (nationalId is null || nationalId.EmployeeID == id)
                    return Result.Success();

                return Result.Failure(new Error("EmployeeAggregateRepository.ValidateNationalIdNumberIsUnique", $"An employee with natioanal ID {nationalIdNumber} is already in the database."));
            }
            catch (Exception ex)
            {
                return Result.Failure(new Error("EmployeeAggregateRepository.ValidateNationalIdNumberIsUnique.", Helpers.GetExceptionMessage(ex)));
            }
        }

        public async Task<Result> ValidateEmployeeEmailIsUnique(int id, string emailAddres, bool asNoTracking = true)
        {
            try
            {
                CancellationToken cancellationToken = default;

                var email = await
                    SpecificationEvaluator.Default.GetQuery
                    (
                        asNoTracking ? _context.Set<PersonDataModel>().AsNoTracking() : _context.Set<PersonDataModel>(),
                        new ValidateEmployeeEmailIsUniqueSpec(emailAddres)
                    ).FirstOrDefaultAsync(cancellationToken);

                if (email is null || email.BusinessEntityID == id)
                    return Result.Success();

                return Result.Failure(new Error("EmployeeAggregateRepository.ValidateEmployeeEmailIsUnique", $"An employee with email address {emailAddres} is already in the database."));
            }
            catch (Exception ex)
            {
                return Result.Failure(new Error("EmployeeAggregateRepository.ValidateEmployeeEmailIsUnique", Helpers.GetExceptionMessage(ex)));
            }
        }

        public async Task<Result> ValidateEmployeeExist(int id, bool asNoTracking = true)
        {
            try
            {
                CancellationToken cancellationToken = default;

                var employeeId = await
                    SpecificationEvaluator.Default.GetQuery
                    (
                        asNoTracking ? _context.Set<EmployeeDataModel>().AsNoTracking() : _context.Set<EmployeeDataModel>(),
                        new ValidateEmployeeExistSpec(id)
                    )
                    .Select(s => new { EmployeeID = s.BusinessEntityID })
                    .FirstOrDefaultAsync(cancellationToken);

                if (employeeId is not null)
                    return Result.Success();

                return Result.Failure(new Error("EmployeeAggregateRepository.ValidateEmployeeExist", $"An employee with ID {id} could not be found."));
            }
            catch (Exception ex)
            {
                return Result.Failure(new Error("EmployeeAggregateRepository.ValidateEmployeeExist", Helpers.GetExceptionMessage(ex)));
            }
        }

        public async Task<Result<EmployeeDomainModel>> GetEmployeeOnlyAsync(int employeeID, bool asNoTracking = false)
        {
            try
            {
                CancellationToken cancellationToken = default;

                var person = await
                    SpecificationEvaluator.Default.GetQuery
                    (
                        asNoTracking ? _context.Set<PersonDataModel>().AsNoTracking() : _context.Set<PersonDataModel>(),
                        new PersonByIDWithEmployeeOnlySpec(employeeID)
                    ).FirstOrDefaultAsync(cancellationToken);

                if (person is not null)
                {
                    EmployeeDomainModel employee = person!.MapToEmployeeDomainObject();
                    return Result<EmployeeDomainModel>.Success<EmployeeDomainModel>(employee);
                }
                else
                {
                    string errMsg = $"An employee with ID: {employeeID} could not be found.";
                    return Result<EmployeeDomainModel>.Failure<EmployeeDomainModel>(new Error("EmployeeAggregateRepository.GetEmployeeOnlyAsync", errMsg));
                }
            }
            catch (Exception ex)
            {
                return Result<EmployeeDomainModel>.Failure<EmployeeDomainModel>(new Error("EmployeeAggregateRepository.GetEmployeeOnlyAsync",
                                                                                          Helpers.GetExceptionMessage(ex)));
            }
        }

        public async Task<Result<EmployeeDomainModel>> GetByIdAsync(int employeeID, bool asNoTracking = false)
        {
            try
            {
                CancellationToken cancellationToken = default;

                var person = await
                    SpecificationEvaluator.Default.GetQuery
                    (
                        asNoTracking ? _context.Set<PersonDataModel>().AsNoTracking() : _context.Set<PersonDataModel>(),
                        new PersonByIDWithEmployeeSpec(employeeID)
                    ).FirstOrDefaultAsync(cancellationToken);

                if (person is not null)
                {
                    EmployeeDomainModel employee = person!.MapToEmployeeDomainObject();

                    // Add addresses to employee from person data model
                    if (person!.BusinessEntityAddresses.ToList().Any())
                    {
                        person!.BusinessEntityAddresses.ToList().ForEach(dataModelAddress =>
                            dataModelAddress.MapDataModelAddressToDomainAddress(ref employee));
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

                    return Result<EmployeeDomainModel>.Success<EmployeeDomainModel>(employee);
                }
                else
                {
                    string errMsg = $"An employee with ID: {employeeID} could not be found.";
                    return Result<EmployeeDomainModel>.Failure<EmployeeDomainModel>(new Error("EmployeeAggregateRepository.GetByIdAsync", errMsg));
                }
            }
            catch (Exception ex)
            {
                return Result<EmployeeDomainModel>.Failure<EmployeeDomainModel>(new Error("EmployeeAggregateRepository.GetByIdAsync",
                                                                                          Helpers.GetExceptionMessage(ex)));
            }
        }

        public async Task<Result<int>> InsertAsync(EmployeeDomainModel employee)
        {
            try
            {
                BusinessEntity entity = new()
                {
                    BusinessEntityID = 0,
                    PersonModel = employee.MapToPersonDataModelForCreate()
                };

                await _context.AddAsync(entity);
                await _unitOfWork.CommitAsync();

                return Result<int>.Success<int>(entity.BusinessEntityID);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure<int>(new Error("EmployeeAggregateRepository.InsertAsync",
                                                           Helpers.GetExceptionMessage(ex)));
            }
        }

        public async Task<Result<int>> Update(EmployeeDomainModel employee)
        {
            try
            {
                CancellationToken cancellationToken = default;

                var person = await
                    SpecificationEvaluator.Default.GetQuery
                    (
                        _context.Set<PersonDataModel>(),
                        new PersonByIDWithEmployeeOnlySpec(employee.Id)
                    ).FirstOrDefaultAsync(cancellationToken);

                if (person is not null)
                {
                    employee.MapToPersonDataModelForUpdate(ref person);

                    await _unitOfWork.CommitAsync();

                    return Result<int>.Success<int>(0);
                }
                else
                {
                    return Result<int>.Failure<int>(new Error("EmployeeAggregateRepository.Update",
                                                              $"Failed to retrieve employee with ID: {employee.Id} for editing."));
                }
            }
            catch (Exception ex)
            {
                return Result<int>.Failure<int>(new Error("EmployeeAggregateRepository.Update",
                                                           Helpers.GetExceptionMessage(ex)));
            }
        }

        public async Task<Result<int>> Delete(EmployeeDomainModel entity)
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
                    return Result<int>.Success<int>(0);
                }
                else
                {
                    return Result<int>.Failure<int>(new Error("EmployeeAggregateRepository.Delete",
                                                              $"Failed to retrieve employee with ID: {entity.Id} for deletion."));
                }
            }
            catch (Exception ex)
            {
                return Result<int>.Failure<int>(new Error("EmployeeAggregateRepository.Delete",
                                                           Helpers.GetExceptionMessage(ex)));
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
    }
}