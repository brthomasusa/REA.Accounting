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

        public const string GetEmployeeDetailsByIdWithAllInfo =
        @"SELECT 
            e.[BusinessEntityID],
            CASE
                WHEN p.NameStyle = 0 THEN 'Western'
                WHEN p.NameStyle = 1 THEN 'Eastern'     
            END AS NameStyle    
            ,p.[Title]
            ,p.[FirstName]
            ,p.[MiddleName]
            ,p.[LastName]
            ,p.[Suffix]
            ,e.[JobTitle]  
            ,pp.[PhoneNumber]
            ,pnt.[Name] AS [PhoneNumberType]
            ,ea.[EmailAddress],
            CASE
                WHEN p.EmailPromotion = 0 THEN 'Does not wish to receive email promotions.'
                WHEN p.EmailPromotion = 1 THEN 'Wishes to receive email promotions from AdventureWorks only.'     
                WHEN p.EmailPromotion = 2 THEN 'Wishes to receive email promotions from AdventureWorks and selected partners.'
            END AS EmailPromotion
            ,e.NationalIDNumber
            ,e.LoginID
            ,a.[AddressLine1]
            ,a.[AddressLine2]
            ,a.[City]
            ,sp.[Name] AS [StateProvinceName] 
            ,a.[PostalCode]
            ,cr.[Name] AS [CountryRegionName]
            ,e.BirthDate
            ,e.MaritalStatus
            ,e.Gender
            ,e.HireDate
            ,e.SalariedFlag AS Salaried
            ,e.VacationHours
            ,e.SickLeaveHours
            ,e.CurrentFlag AS Active 
        FROM [HumanResources].[Employee] e
        INNER JOIN [Person].[Person] p ON p.[BusinessEntityID] = e.[BusinessEntityID]        
        INNER JOIN [Person].[BusinessEntityAddress] bea ON bea.[BusinessEntityID] = e.[BusinessEntityID]         
        INNER JOIN [Person].[Address] a ON a.[AddressID] = bea.[AddressID]        
        INNER JOIN [Person].[StateProvince] sp ON sp.[StateProvinceID] = a.[StateProvinceID]        
        INNER JOIN [Person].[CountryRegion] cr ON cr.[CountryRegionCode] = sp.[CountryRegionCode]        
        LEFT OUTER JOIN [Person].[PersonPhone] pp ON pp.BusinessEntityID = p.[BusinessEntityID]        
        LEFT OUTER JOIN [Person].[PhoneNumberType] pnt ON pp.[PhoneNumberTypeID] = pnt.[PhoneNumberTypeID]        
        LEFT OUTER JOIN [Person].[EmailAddress] ea ON p.[BusinessEntityID] = ea.[BusinessEntityID]";

        public const string GetEmployeeListItems =
        @"SELECT 
            e.[BusinessEntityID]           
            ,p.[LastName]
            ,p.[FirstName]
            ,p.[MiddleName] 
            ,e.[JobTitle]
            ,d.[Name] AS [Department]
            ,pp.[PhoneNumber] 
            ,ea.[EmailAddress]
            ,e.CurrentFlag AS Active 
        FROM [HumanResources].[Employee] e
        INNER JOIN [Person].[Person] p ON p.[BusinessEntityID] = e.[BusinessEntityID]
        LEFT OUTER JOIN [Person].[PersonPhone] pp ON pp.BusinessEntityID = p.[BusinessEntityID]	
        INNER JOIN [HumanResources].[EmployeeDepartmentHistory] edh ON e.[BusinessEntityID] = edh.[BusinessEntityID]     
        INNER JOIN [HumanResources].[Department] d ON edh.[DepartmentID] = d.[DepartmentID]        
        LEFT OUTER JOIN [Person].[EmailAddress] ea ON p.[BusinessEntityID] = ea.[BusinessEntityID]";

        public const string GetEmployeeListItemsCount =
        @"SELECT 
            COUNT(*) 
        FROM Person.Person p
        INNER JOIN HumanResources.Employee e ON e.BusinessEntityID = p.BusinessEntityID        
        WHERE p.LastName LIKE CONCAT('%',@LName,'%')";
    }
}