using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.Organization.GetCompany
{
    public sealed class GetCompanyDetailByIdQueryHandler : IQueryHandler<GetCompanyDetailByIdRequest, GetCompanyDetailByIdResponse>
    {
        private readonly IReadRepositoryManager _repo;

        public GetCompanyDetailByIdQueryHandler(IReadRepositoryManager repo)
            => _repo = repo;

        public async Task<Result<GetCompanyDetailByIdResponse>> Handle
        (
            GetCompanyDetailByIdRequest request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                Result<GetCompanyDetailByIdResponse> result = await _repo.CompanyReadRepository.GetCompanyDetailsById(request.CompanyID);

                if (result.IsFailure)
                {
                    return Result<GetCompanyDetailByIdResponse>.Failure<GetCompanyDetailByIdResponse>(
                        new Error("GetCompanyDetailByIdQueryHandler.Handle.Handle", result.Error.Message)
                    );
                }

                return result.Value;

            }
            catch (Exception ex)
            {
                return Result<GetCompanyDetailByIdResponse>.Failure<GetCompanyDetailByIdResponse>(
                    new Error("GetCompanyDetailByIdQueryHandler.Handle", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}