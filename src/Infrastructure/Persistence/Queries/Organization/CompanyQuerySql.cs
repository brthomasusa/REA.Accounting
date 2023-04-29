namespace REA.Accounting.Infrastructure.Persistence.Queries.Organization
{
    public static class CompanyQuerySql
    {
        public const string GetCompanyDetailsById =
        @"SELECT 
            CompanyID, CompanyName, LegalName, EIN, WebsiteUrl AS 'CompanyWebSite', 
            MailAddressLine1, MailAddressLine2, MailCity, mail.StateProvinceCode  AS MailStateProvinceCode, MailPostalCode,
            DeliveryAddressLine1, DeliveryAddressLine2, DeliveryCity, delivery.StateProvinceCode AS DeliveryStateProvinceCode, 
            DeliveryPostalCode, Telephone, Fax    
        FROM Person.Company company
        INNER JOIN Person.StateProvince mail ON company.MailStateProvinceID = mail.StateProvinceID
        INNER JOIN Person.StateProvince delivery ON company.DeliveryStateProvinceID = delivery.StateProvinceID";

        public const string GetCompanyCommandById =
        @"SELECT 
            CompanyID, CompanyName, LegalName, EIN, WebsiteUrl AS 'CompanyWebSite', 
            MailAddressLine1, MailAddressLine2, MailCity, MailStateProvinceID, MailPostalCode,
            DeliveryAddressLine1, DeliveryAddressLine2, DeliveryCity, DeliveryStateProvinceID, 
            DeliveryPostalCode, Telephone, Fax   
        FROM Person.Company company";

        public const string GetCompanyDepartments =
        @"SELECT 
            DepartmentID, [Name], GroupName, ModifiedDate 
        FROM HumanResources.Department   
        ORDER BY [Name]";

        public const string GetCompanyDepartmentsByName =
        @"SELECT 
            DepartmentID, [Name], GroupName, ModifiedDate 
        FROM HumanResources.Department";

        public const string GetCompanyShifts =
        @"SELECT
            ShiftID,
            Name,
            CONVERT(nvarchar(8), StartTime) AS StartTime,
            CONVERT(nvarchar(8), EndTime) AS EndTime,
            ModifiedDate
        FROM HumanResources.Shift
        ORDER BY ShiftID";
    }
}