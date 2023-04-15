using Microsoft.Extensions.Logging;
using System.Data;
using Dapper;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.Infrastructure.Persistence.Repositories;

namespace REA.Accounting.Infrastructure.Persistence.Queries.Organization
{
    public static class GetCompanyDepartmentsQuery
    {
        private static int Offset(int page, int pageSize) => (page - 1) * pageSize;

        public async static Task<Result<PagedList<GetCompanyDepartmentsResponse>>> Query
        (
            PagingParameters pagingParameters,
            DapperContext ctx,
            ILogger<ReadRepositoryManager> logger
        )
        {
            try
            {
                const string sql = CompanyQuerySql.GetCompanyDepartments;

                var parameters = new DynamicParameters();
                parameters.Add("Offset", Offset(pagingParameters.PageNumber, pagingParameters.PageSize), DbType.Int32);
                parameters.Add("PageSize", pagingParameters.PageSize, DbType.Int32);

                using var connection = ctx.CreateConnection();

                var items = await connection.QueryAsync<GetCompanyDepartmentsResponse>(sql, parameters);
                int count = items.Count();

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