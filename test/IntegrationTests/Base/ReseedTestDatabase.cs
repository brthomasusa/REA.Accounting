using System;
using System.Data.SqlClient;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.IntegrationTests.Base
{
    public static class ReseedTestDatabase
    {
        public static OperationResult<bool> ReseedDatabase()
        {
            const string connectionString = "Server=tcp:mssql-server,1433;Database=AdventureWorks_Test;User Id=sa;Password=Info99Gum;MultipleActiveResultSets=true;TrustServerCertificate=true";

            try
            {
                using SqlConnection connection = new(connectionString);
                SqlCommand command = new("dbo.usp_InitializeTestDb", connection);
                command.Connection.Open();
                command.ExecuteNonQuery();

                return OperationResult<bool>.CreateSuccessResult(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }
    }
}