using Microsoft.Extensions.Logging;
using System.Data;
using Dapper;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.Infrastructure.Persistence.Repositories;

namespace REA.Accounting.Infrastructure.Persistence.Queries.Organization
{
    public static class GetCompanyCommandByIdQuery
    {
        public async static Task<Result<GetCompanyCommandByIdResponse>> Query
        (
            int companyId,
            DapperContext ctx,
            ILogger<ReadRepositoryManager> logger
        )
        {
            try
            {
                const string sql = CompanyQuerySql.GetCompanyCommandById +
                " WHERE CompanyID = @ID";

                var parameters = new DynamicParameters();
                parameters.Add("ID", companyId, DbType.Int32);

                using var connection = ctx.CreateConnection();
                GetCompanyCommandByIdResponse detail = await connection.QueryFirstOrDefaultAsync<GetCompanyCommandByIdResponse>(sql, parameters);

                if (detail is null)
                {
                    string errMsg = $"Unable to retrieve company command for company with ID: {companyId}.";
                    logger.LogWarning($"Code Path: GetCompanyCommandsByIdQuery.Query - Message: {errMsg}");

                    return Result<GetCompanyCommandByIdResponse>.Failure<GetCompanyCommandByIdResponse>(
                        new Error("GetCompanyCommandByIdQuery.Query", errMsg)
                    );
                }

                return detail;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Code Path: GetCompanyCommandByIdQuery.Query - Message: {Helpers.GetExceptionMessage(ex)}");
                return Result<GetCompanyCommandByIdResponse>.Failure<GetCompanyCommandByIdResponse>(
                    new Error("GetCompanyCommandByIdQuery.Query", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}