namespace REA.Accounting.Infrastructure.Persistence.Queries.HumanResources
{
    public static class EmployeeQuerySql
    {
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
            ,CONCAT(p.FirstName,' ',COALESCE(p.MiddleName,''),' ',p.LastName) as FullName 
            , HumanResources.Get_Manager_ID(e.BusinessEntityID) AS ManagerID
            ,HumanResources.GetNumberOfEmployeesManaged(e.BusinessEntityID) AS EmployeesManaged 
            ,HumanResources.GetManagerFullName(HumanResources.Get_Manager_ID(e.BusinessEntityID)) AS ManagerName                       
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