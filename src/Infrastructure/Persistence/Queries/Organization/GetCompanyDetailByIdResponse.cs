namespace REA.Accounting.Infrastructure.Persistence.Queries.Organization
{
    public class GetCompanyDetailByIdResponse
    {
        public int CompanyID { get; set; }
        public string? CompanyName { get; set; }
        public string? LegalName { get; set; }
        public string? EIN { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? MailAddressLine1 { get; set; }
        public string? MailAddressLine2 { get; set; }
        public string? MailCity { get; set; }
        public string? MailStateProvinceCode { get; set; }
        public string? MailPostalCode { get; set; }
        public string? DeliveryAddressLine1 { get; set; }
        public string? DeliveryAddressLine2 { get; set; }
        public string? DeliveryCity { get; set; }
        public string? DeliveryStateProvinceCode { get; set; }
        public string? DeliveryPostalCode { get; set; }
        public string? Telephone { get; set; }
        public string? Fax { get; set; }
    }
}