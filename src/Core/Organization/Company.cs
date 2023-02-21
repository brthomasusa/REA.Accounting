#pragma warning disable CS8618

using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.CommonValueObjects;

namespace REA.Accounting.Core.Organization
{
    public sealed class Company : AggregateRoot<int>
    {
        private Company
        (
            int companyID,
            OrganizationName companyName,
            OrganizationName? legalName,
            EmployerIdentificationNumber ein,
            WebsiteUrl? url
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
            string companyWebSite
        )
            => new(
                companyID,
                OrganizationName.Create(companyName),
                OrganizationName.Create(legalName),
                EmployerIdentificationNumber.Create(ein),
                WebsiteUrl.Create(companyWebSite)
            );

        public string CompanyName { get; init; }
        public string? LegalName { get; init; }
        public string EIN { get; init; }
        public string? CompanyWebSite { get; init; }
    }
}