using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.Organization.GetCompany
{
    public sealed class GetCompanyCommandByIdQueryHandler : IQueryHandler<GetCompanyCommandByIdRequest, GetCompanyCommandByIdResponse>
    {
        private readonly IReadRepositoryManager _repo;

        public GetCompanyCommandByIdQueryHandler(IReadRepositoryManager repo)
            => _repo = repo;

        public async Task<Result<GetCompanyCommandByIdResponse>> Handle
        (
            GetCompanyCommandByIdRequest request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                Result<GetCompanyCommandByIdResponse> result = await _repo.CompanyReadRepository.GetCompanyCommandById(request.CompanyID);

                if (result.IsFailure)
                {
                    return Result<GetCompanyCommandByIdResponse>.Failure<GetCompanyCommandByIdResponse>(
                        new Error("GetCompanyCommandByIdQueryHandler.Handle", result.Error.Message)
                    );
                }

                return result.Value;

            }
            catch (Exception ex)
            {
                return Result<GetCompanyCommandByIdResponse>.Failure<GetCompanyCommandByIdResponse>(
                    new Error("GetCompanyCommandByIdQueryHandler.Handle", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}