using Microsoft.Extensions.Logging;
using Dapper;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.Shared.Models.Lookups;
using REA.Accounting.Infrastructure.Persistence.Repositories;

namespace REA.Accounting.Infrastructure.Persistence.Queries.Lookups
{
    public static class GetStateCodeIdForUSAQuery
    {
        public async static Task<Result<List<StateCode>>> Query
        (
            DapperContext ctx,
            ILogger<ReadRepositoryManager> logger
        )
        {
            try
            {
                const string sql = LookupsQuerySql.GetStateCodeIdForUSA;

                using var connection = ctx.CreateConnection();
                var stateCodes = await connection.QueryAsync<StateCode>(sql);

                if (stateCodes is null)
                {
                    return Result<List<StateCode>>.Failure<List<StateCode>>(
                        new Error("GetStateCodeIdForUSAQuery.Query", "Oops! No U.S. state province codes found.")
                    );
                }

                return stateCodes.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Code Path: GetStateCodeIdForUSAQuery.Query - Message: {Helpers.GetExceptionMessage(ex)}");
                return Result<List<StateCode>>.Failure<List<StateCode>>(
                    new Error("GetStateCodeIdForUSAQuery.Query", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}