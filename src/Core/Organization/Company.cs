#pragma warning disable CS8618

using REA.Accounting.Core.Shared;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.HumanResources.ValueObjects;
using REA.Accounting.SharedKernel;
using REA.Accounting.SharedKernel.CommonValueObjects;

namespace REA.Accounting.Core.Organization
{
    public class Company : AggregateRoot<int>
    {
        private List<Employee> _employees = new();

        private Company
        (
            int companyID,
            OrganizationName companyName,
            OrganizationName? legalName,
            EmployerIdentificationNumber ein,
            WebsiteUrl? url,
            List<Employee> employees
        )
        {
            Id = companyID;
            CompanyName = companyName.Value!;
            LegalName = legalName!.Value;
            EIN = ein.Value!;
            CompanyWebSite = url!.Value;
        }

        public static Company Create
        (
            int companyID,
            string companyName,
            string legalName,
            string ein,
            string companyWebSite,
            List<Employee> employees
        )
            => new Company
            (
                companyID,
                OrganizationName.Create(companyName),
                OrganizationName.Create(legalName),
                EmployerIdentificationNumber.Create(ein),
                WebsiteUrl.Create(companyWebSite),
                employees
            );

        protected string CompanyName { get; init; }
        protected string? LegalName { get; init; }
        protected string EIN { get; init; }
        protected string? CompanyWebSite { get; init; }

        public void CreateShift(int id, string name, int startHour, int startMinute, int endHour, int endMinute)
            => Shift.Create(id, name, startHour, startMinute, endHour, endMinute);

        public void AddEmployeeToShift(Employee employee, Shift shift)
        {

        }

        public void ChangeEmployeeShift(Employee employee, Shift currentShift, Shift newShift)
        {

        }

        public void CreateDepartment(int id, string name, string groupName)
            => Department.Create
            (
                id,
                OrganizationName.Create(name),
                OrganizationName.Create(groupName)
            );

        public void AddDepartmentMember(Employee employee)
        {

        }

        public void ChangeEmployeeDepartmentMembsership
        (
            Employee employee,
            Department currentDept,
            Department newDept
        )
        {

        }

        public void HireEmployee
        (
            int employeeID,
            string personType,
            NameStyleEnum nameStyle,
            string title,
            string firstName,
            string lastName,
            string middleName,
            string suffix,
            string nationalID,
            string login,
            string orgNode,
            string jobTitle,
            DateOnly birthDate,
            string maritalStatus,
            string gender,
            DateOnly hireDate,
            bool salaried,
            int vacation,
            int sickLeave,
            bool active
        )
        => Employee.Create
        (
            employeeID,
            personType,
            nameStyle,
            title,
            firstName,
            lastName,
            middleName,
            suffix,
            nationalID,
            login,
            orgNode,
            jobTitle,
            birthDate,
            maritalStatus,
            gender,
            hireDate,
            salaried,
            vacation,
            sickLeave,
            active
        );

        public void TerminateEmployee(Employee employee)
        {

        }
    }
}