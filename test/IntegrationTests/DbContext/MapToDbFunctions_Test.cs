using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;

namespace REA.Accounting.IntegrationTests.DbContext
{
    public class MapToDbFunctions_Test : TestBase
    {
        [Fact]
        public void EfCoreContext_Get_Manager_ID_ForCEO()
        {
            //SETUP
            const int employeeID = 1;

            var query1 = from emp in _dbContext.Employee
                         where emp.BusinessEntityID == employeeID
                         select new EmployeeDataModel
                         {
                             BusinessEntityID = emp.BusinessEntityID,
                             ManagerID = _dbContext.Get_Manager_ID(emp.BusinessEntityID),
                             NationalIDNumber = emp.NationalIDNumber,
                             LoginID = emp.LoginID,
                             JobTitle = emp.JobTitle,
                             BirthDate = emp.BirthDate,
                             MaritalStatus = emp.MaritalStatus,
                             Gender = emp.Gender,
                             HireDate = emp.HireDate,
                             SalariedFlag = emp.SalariedFlag,
                             VacationHours = emp.VacationHours,
                             SickLeaveHours = emp.SickLeaveHours,
                             CurrentFlag = emp.CurrentFlag,
                             ModifiedDate = emp.ModifiedDate
                         };

            //ATTEMPT
            var result = query1.FirstOrDefault();

            //VERIFY
            Assert.Equal(employeeID, result!.ManagerID);
        }

        [Fact]
        public void EfCoreContext_Get_Manager_ID_ForAnEmployeeOneLevelUnderCEO()
        {
            //SETUP
            const int employeeID = 2;

            var query1 = from emp in _dbContext.Employee
                         where emp.BusinessEntityID == employeeID
                         select new EmployeeDataModel
                         {
                             BusinessEntityID = emp.BusinessEntityID,
                             ManagerID = _dbContext.Get_Manager_ID(emp.BusinessEntityID),
                             NationalIDNumber = emp.NationalIDNumber,
                             LoginID = emp.LoginID,
                             JobTitle = emp.JobTitle,
                             BirthDate = emp.BirthDate,
                             MaritalStatus = emp.MaritalStatus,
                             Gender = emp.Gender,
                             HireDate = emp.HireDate,
                             SalariedFlag = emp.SalariedFlag,
                             VacationHours = emp.VacationHours,
                             SickLeaveHours = emp.SickLeaveHours,
                             CurrentFlag = emp.CurrentFlag,
                             ModifiedDate = emp.ModifiedDate
                         };

            //ATTEMPT
            var result = query1.FirstOrDefault();

            //VERIFY
            Assert.Equal(1, result!.ManagerID);
        }

        [Fact]
        public void EfCoreContext_Get_Manager_ID_ForAnEmployeeMoreThanOneLevelUnderCEO()
        {
            //SETUP
            const int employeeID = 14;

            var query1 = from emp in _dbContext.Employee
                         where emp.BusinessEntityID == employeeID
                         select new EmployeeDataModel
                         {
                             BusinessEntityID = emp.BusinessEntityID,
                             ManagerID = _dbContext.Get_Manager_ID(emp.BusinessEntityID),
                             NationalIDNumber = emp.NationalIDNumber,
                             LoginID = emp.LoginID,
                             JobTitle = emp.JobTitle,
                             BirthDate = emp.BirthDate,
                             MaritalStatus = emp.MaritalStatus,
                             Gender = emp.Gender,
                             HireDate = emp.HireDate,
                             SalariedFlag = emp.SalariedFlag,
                             VacationHours = emp.VacationHours,
                             SickLeaveHours = emp.SickLeaveHours,
                             CurrentFlag = emp.CurrentFlag,
                             ModifiedDate = emp.ModifiedDate
                         };

            //ATTEMPT
            var result = query1.FirstOrDefault();

            //VERIFY
            Assert.Equal(3, result!.ManagerID);
        }

        [Fact]
        public void Get_Manager_ID()
        {
            //SETUP
            const int employeeID = 14;

            var query1 = from emp in _dbContext.Employee
                         where emp.BusinessEntityID == employeeID
                         select new
                         {
                             ManagerID = _dbContext.Get_Manager_ID(emp.BusinessEntityID)
                         };

            //ATTEMPT
            var result = query1.FirstOrDefault();

            //VERIFY
            Assert.Equal(3, result!.ManagerID);
        }
    }
}