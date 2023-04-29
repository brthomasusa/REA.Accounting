using Microsoft.Extensions.Logging;
using System.Data;
using Dapper;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.Infrastructure.Persistence.Repositories;

namespace REA.Accounting.Infrastructure.Persistence.Queries.HumanResources
{
    public static class GetEmployeeDetailsByIdWithAllInfoQuery
    {
        public async static Task<Result<GetEmployeeDetailsByIdWithAllInfoResponse>> Query
        (
            int employeeId,
            DapperContext ctx,
            ILogger<ReadRepositoryManager> logger
        )
        {
            try
            {
                const string sql = EmployeeQuerySql.GetEmployeeDetailsByIdWithAllInfo +
                " WHERE e.BusinessEntityID = @ID";

                var parameters = new DynamicParameters();
                parameters.Add("ID", employeeId, DbType.Int32);

                using var connection = ctx.CreateConnection();
                GetEmployeeDetailsByIdWithAllInfoResponse detail =
                    await connection.QueryFirstOrDefaultAsync<GetEmployeeDetailsByIdWithAllInfoResponse>(sql, parameters);

                if (detail is null)
                {
                    string errMsg = $"Unable to retrieve employee details for employee with ID: {employeeId}.";
                    logger.LogWarning($"Code Path: GetEmployeeDetailsByIdWithAllInfoQuery.Query - Message: {errMsg}");

                    return Result<GetEmployeeDetailsByIdWithAllInfoResponse>.Failure<GetEmployeeDetailsByIdWithAllInfoResponse>(
                        new Error("GetEmployeeDetailsByIdWithAllInfoQuery.Query", errMsg)
                    );
                }

                return detail;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Code Path: GetEmployeeDetailsByIdWithAllInfoQuery.Query - Message: {Helpers.GetExceptionMessage(ex)}");
                return Result<GetEmployeeDetailsByIdWithAllInfoResponse>.Failure<GetEmployeeDetailsByIdWithAllInfoResponse>(
                    new Error("GetEmployeeDetailsByIdWithAllInfoQuery.Query", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}