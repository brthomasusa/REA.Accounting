#pragma warning disable CS8618

using REA.Accounting.SharedKernel;
using REA.Accounting.SharedKernel.CommonValueObjects;

namespace REA.Accounting.Core.Organization
{
    public class Company : AggregateRoot<int>
    {
        protected Company() { }

        public Company
        (
            int companyID,
            OrganizationName companyName,
            OrganizationName? legalName,
            EmployerIdentificationNumber ein,
            WebsiteUrl? url
        ) : this()
        {
            Id = companyID;
            CompanyName = companyName ?? throw new ArgumentNullException("A company name is required.");
            LegalName = legalName;
            EIN = ein ?? throw new ArgumentNullException("An employer identification number (EIN) is required.");
            CompanyWebSite = url;
        }

        public OrganizationName CompanyName { get; init; }
        public OrganizationName? LegalName { get; init; }
        public EmployerIdentificationNumber EIN { get; init; }
        public WebsiteUrl? CompanyWebSite { get; init; }
    }
}