using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.Shared.Models.HumanResources;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Infrastructure.Persistence.Queries.HumanResources
{
    public static class GetEmployeeDetailsByIdWithAllInfoQuery
    {
        public async static Task<Result<EmployeeDetailReadModel>> Query
        (
            int employeeId,
            DapperContext ctx,
            ILogger<ReadRepositoryManager> logger
        )
        {
            try
            {
                const string sql = "SELECT * FROM HumanResources.udfGetEmployeeDetails(@ID)";
                var parameters = new DynamicParameters();
                parameters.Add("ID", employeeId, DbType.Int32);

                using var connection = ctx.CreateConnection();
                EmployeeDetailReadModel detail = await connection.QueryFirstOrDefaultAsync<EmployeeDetailReadModel>(sql, parameters);

                if (detail is null)
                {
                    string errMsg = $"Unable to retrieve employee details for employee with ID: {employeeId}.";
                    logger.LogWarning($"Code Path: GetEmployeeDetailsByIdWithAllInfoQuery.Query - Message: {errMsg}");

                    return Result<EmployeeDetailReadModel>.Failure<EmployeeDetailReadModel>(
                        new Error("GetEmployeeDetailsByIdWithAllInfoQuery.Query", errMsg)
                    );
                }

                return detail;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Code Path: GetEmployeeDetailsByIdWithAllInfoQuery.Query - Message: {Helpers.GetExceptionMessage(ex)}");
                return Result<EmployeeDetailReadModel>.Failure<EmployeeDetailReadModel>(
                    new Error("GetEmployeeDetailsByIdWithAllInfoQuery.Query", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}
