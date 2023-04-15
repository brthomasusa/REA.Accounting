using Microsoft.Extensions.Logging;
using System.Data;
using Dapper;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.Infrastructure.Persistence.Repositories;

namespace REA.Accounting.Infrastructure.Persistence.Queries.Organization
{
    public static class GetCompanyShiftsQuery
    {
        private static int Offset(int page, int pageSize) => (page - 1) * pageSize;

        public async static Task<Result<PagedList<GetCompanyShiftsResponse>>> Query
        (
            PagingParameters pagingParameters,
            DapperContext ctx,
            ILogger<ReadRepositoryManager> logger
        )
        {
            try
            {
                const string sql = CompanyQuerySql.GetCompanyShifts;

                var parameters = new DynamicParameters();
                parameters.Add("Offset", Offset(pagingParameters.PageNumber, pagingParameters.PageSize), DbType.Int32);
                parameters.Add("PageSize", pagingParameters.PageSize, DbType.Int32);

                using var connection = ctx.CreateConnection();

                var items = await connection.QueryAsync<GetCompanyShiftsResponse>(sql, parameters);
                int count = items.Count();

                var pagedList = PagedList<GetCompanyShiftsResponse>.CreatePagedList(
                        items.ToList(), count, pagingParameters.PageNumber, pagingParameters.PageSize
                    );

                return pagedList;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Code Path: GetCompanyShiftsQuery.Query - Message: {Helpers.GetExceptionMessage(ex)}");

                return Result<PagedList<GetCompanyShiftsResponse>>.Failure<PagedList<GetCompanyShiftsResponse>>(
                    new Error("GetCompanyShiftsQuery.Query", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}