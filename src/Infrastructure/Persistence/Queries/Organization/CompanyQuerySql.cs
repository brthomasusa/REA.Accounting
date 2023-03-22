namespace REA.Accounting.Infrastructure.Persistence.Queries.Organization
{
    public static class CompanyQuerySql
    {
        public const string GetCompanyDetailsById =
        @"SELECT 
            CompanyID, CompanyName, LegalName, EIN, WebsiteUrl, 
            MailAddressLine1, MailAddressLine2, MailCity, mail.StateProvinceCode  AS MailStateProvinceCode, MailPostalCode,
            DeliveryAddressLine1, DeliveryAddressLine2, DeliveryCity, delivery.StateProvinceCode AS DeliveryStateProvinceCode, 
            DeliveryPostalCode, Telephone, Fax    
        FROM Person.Company company
        INNER JOIN Person.StateProvince mail ON company.MailStateProvinceID = mail.StateProvinceID
        INNER JOIN Person.StateProvince delivery ON company.DeliveryStateProvinceID = delivery.StateProvinceID";

        public const string GetCompanyCommandById =
        @"SELECT 
            CompanyID, CompanyName, LegalName, EIN, WebsiteUrl, 
            MailAddressLine1, MailAddressLine2, MailCity, MailStateProvinceID, MailPostalCode,
            DeliveryAddressLine1, DeliveryAddressLine2, DeliveryCity, DeliveryStateProvinceID, 
            DeliveryPostalCode, Telephone, Fax   
        FROM Person.Company company";
    }
}