using Microsoft.Extensions.Logging;
using System.Data;
using Dapper;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.Shared.Models.HumanResources;

namespace REA.Accounting.Infrastructure.Persistence.Queries.HumanResources
{
    public static class GetEmployeeListItemsQuery
    {
        private static int Offset(int page, int pageSize) => (page - 1) * pageSize;

        public async static Task<Result<PagedList<EmployeeListItemReadModel>>> Query
        (
            string lastName,
            PagingParameters pagingParameters,
            DapperContext ctx,
            ILogger<ReadRepositoryManager> logger
        )
        {
            try
            {
                const string sql = EmployeeQuerySql.GetEmployeeListItems +
                    " WHERE edh.EndDate IS NULL AND p.LastName LIKE CONCAT('%',@LName,'%')" +
                    " ORDER BY p.LastName, p.FirstName, p.MiddleName" +
                    " OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

                var parameters = new DynamicParameters();
                parameters.Add("LName", lastName, DbType.String);
                parameters.Add("Offset", Offset(pagingParameters.PageNumber, pagingParameters.PageSize), DbType.Int32);
                parameters.Add("PageSize", pagingParameters.PageSize, DbType.Int32);

                const string countSql = EmployeeQuerySql.GetEmployeeListItemsCount;

                using var connection = ctx.CreateConnection();

                var items = await connection.QueryAsync<EmployeeListItemReadModel>(sql, parameters);
                int count = connection.ExecuteScalar<int>(countSql, parameters);

                var pagedList = PagedList<EmployeeListItemReadModel>.CreatePagedList(
                        items.ToList(), count, pagingParameters.PageNumber, pagingParameters.PageSize
                    );

                return pagedList;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Code Path: GetEmployeeListItemsQuery.Query - Message: {Helpers.GetExceptionMessage(ex)}");

                return Result<PagedList<EmployeeListItemReadModel>>.Failure<PagedList<EmployeeListItemReadModel>>(
                    new Error("GetEmployeeListItemsQuery.Query", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}