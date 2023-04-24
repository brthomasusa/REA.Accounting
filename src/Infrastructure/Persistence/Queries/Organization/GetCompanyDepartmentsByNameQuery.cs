using Microsoft.Extensions.Logging;
using System.Data;
using Dapper;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.Infrastructure.Persistence.Repositories;

namespace REA.Accounting.Infrastructure.Persistence.Queries.Organization
{
    public static class GetCompanyDepartmentsByNameQuery
    {
        private static int Offset(int page, int pageSize) => (page - 1) * pageSize;

        public async static Task<Result<PagedList<GetCompanyDepartmentsResponse>>> Query
        (
            string departmentName,
            PagingParameters pagingParameters,
            DapperContext ctx,
            ILogger<ReadRepositoryManager> logger
        )
        {
            try
            {
                const string sql = CompanyQuerySql.GetCompanyDepartmentsByName +
                    " WHERE [Name] LIKE CONCAT('%',@DeptName,'%')" +
                    " ORDER BY [Name]" +
                    " OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

                var parameters = new DynamicParameters();
                parameters.Add("DeptName", departmentName, DbType.String);
                parameters.Add("Offset", Offset(pagingParameters.PageNumber, pagingParameters.PageSize), DbType.Int32);
                parameters.Add("PageSize", pagingParameters.PageSize, DbType.Int32);

                const string countSql = "SELECT COUNT(*) FROM HumanResources.Department WHERE [Name] LIKE CONCAT('%',@DeptName,'%')";

                using var connection = ctx.CreateConnection();

                var items = await connection.QueryAsync<GetCompanyDepartmentsResponse>(sql, parameters);
                int count = connection.ExecuteScalar<int>(countSql, parameters);

                var pagedList = PagedList<GetCompanyDepartmentsResponse>.CreatePagedList(
                        items.ToList(), count, pagingParameters.PageNumber, pagingParameters.PageSize
                    );

                return pagedList;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Code Path: GetCompanyDepartmentsQuery.Query - Message: {Helpers.GetExceptionMessage(ex)}");

                return Result<PagedList<GetCompanyDepartmentsResponse>>.Failure<PagedList<GetCompanyDepartmentsResponse>>(
                    new Error("GetCompanyDepartmentsQuery.Query", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}