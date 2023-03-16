namespace REA.Accounting.Infrastructure.Persistence.Queries.HumanResources
{
    public static class EmployeeQuerySql
    {
        public const string GetEmployeeDetailsById =
        @"SELECT 
            entity.BusinessEntityID AS EmployeeID,
            CASE
                WHEN person.NameStyle = 0 THEN 'Western'
                WHEN person.NameStyle = 1 THEN 'Eastern'     
            END AS NameStyle,     
            person.Title, person.FirstName, person.MiddleName, person.LastName, person.Suffix,
            CASE
                WHEN person.EmailPromotion = 0 THEN 'Does not wish to receive email promotions.'
                WHEN person.EmailPromotion = 1 THEN 'Wishes to receive email promotions from AdventureWorks only.'     
                WHEN person.EmailPromotion = 2 THEN 'Wishes to receive email promotions from AdventureWorks and selected partners.'
            END AS EmailPromotion,
            ee.NationalIDNumber, ee.LoginID, ee.JobTitle, ee.BirthDate, ee.MaritalStatus, ee.Gender,
            ee.HireDate, ee.SalariedFlag AS Salaried, ee.VacationHours, ee.SickLeaveHours, ee.CurrentFlag as Active
        FROM Person.BusinessEntity entity
        INNER JOIN Person.Person person ON entity.BusinessEntityID = person.BusinessEntityID
        INNER JOIN HumanResources.Employee ee ON entity.BusinessEntityID = ee.BusinessEntityID ";
    }
}