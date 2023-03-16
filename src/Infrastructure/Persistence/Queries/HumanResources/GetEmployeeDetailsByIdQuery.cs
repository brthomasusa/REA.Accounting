using Microsoft.Extensions.Logging;
using System.Data;
using Dapper;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.Infrastructure.Persistence.Repositories;

namespace REA.Accounting.Infrastructure.Persistence.Queries.HumanResources
{
    public static class GetEmployeeDetailsByIdQuery
    {
        public async static Task<Result<GetEmployeeDetailByIdResponse>> Query
        (
            int employeeId,
            DapperContext ctx,
            ILogger<ReadRepositoryManager> logger
        )
        {
            try
            {
                const string sql = EmployeeQuerySql.GetEmployeeDetailsById +
                " WHERE entity.BusinessEntityID = @ID";

                var parameters = new DynamicParameters();
                parameters.Add("ID", employeeId, DbType.Int32);

                using var connection = ctx.CreateConnection();
                GetEmployeeDetailByIdResponse detail = await connection.QueryFirstOrDefaultAsync<GetEmployeeDetailByIdResponse>(sql, parameters);

                if (detail is null)
                {
                    string errMsg = $"Unable to retrieve employee details for employee with ID: {employeeId}.";
                    logger.LogWarning($"Code Path: GetEmployeeDetailsByIdQuery.Query - Message: {errMsg}");

                    return Result<GetEmployeeDetailByIdResponse>.Failure<GetEmployeeDetailByIdResponse>(
                        new Error("GetEmployeeDetailsByIdQuery.Query", errMsg)
                    );
                }

                return detail;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Code Path: GetEmployeeDetailsByIdQuery.Query - Message: {Helpers.GetExceptionMessage(ex)}");
                return Result<GetEmployeeDetailByIdResponse>.Failure<GetEmployeeDetailByIdResponse>(
                    new Error("GetEmployeeDetailsByIdQuery.Query", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}