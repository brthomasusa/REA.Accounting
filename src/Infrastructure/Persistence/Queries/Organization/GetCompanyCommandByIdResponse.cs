namespace REA.Accounting.Infrastructure.Persistence.Queries.Organization
{
    public class GetCompanyCommandByIdResponse
    {
        public int CompanyID { get; set; }
        public string? CompanyName { get; set; }
        public string? LegalName { get; set; }
        public string? EIN { get; set; }
        public string? CompanyWebSite { get; set; }
        public string? MailAddressLine1 { get; set; }
        public string? MailAddressLine2 { get; set; }
        public string? MailCity { get; set; }
        public int MailStateProvinceID { get; set; }
        public string? MailPostalCode { get; set; }
        public string? DeliveryAddressLine1 { get; set; }
        public string? DeliveryAddressLine2 { get; set; }
        public string? DeliveryCity { get; set; }
        public int DeliveryStateProvinceID { get; set; }
        public string? DeliveryPostalCode { get; set; }
        public string? Telephone { get; set; }
        public string? Fax { get; set; }
    }
}