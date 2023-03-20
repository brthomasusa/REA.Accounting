using System.ComponentModel.DataAnnotations;

namespace REA.Accounting.Shared.Models.Organization
{
    public class CompanyDetailModel : IModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string LegalName { get; set; } = string.Empty;
        public string EIN { get; set; } = string.Empty;
        public string CompanyWebSite { get; set; } = string.Empty;
        public string MailAddressLine1 { get; set; } = string.Empty;
        public string MailAddressLine2 { get; set; } = string.Empty;
        public string MailCity { get; set; } = string.Empty;
        public string MailStateProvinceCode { get; set; } = string.Empty;
        public string MailPostalCode { get; set; } = string.Empty;
        public string DeliveryAddressLine1 { get; set; } = string.Empty;
        public string DeliveryAddressLine2 { get; set; } = string.Empty;
        public string DeliveryCity { get; set; } = string.Empty;
        public string DeliveryStateProvinceCode { get; set; } = string.Empty;
        public string DeliveryPostalCode { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;
    }
}