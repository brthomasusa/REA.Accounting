using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.Organization.GetCompanyById
{
    public sealed class GetCompanyByIdQueryHandler : IQueryHandler<GetCompanyByIdRequest, GetCompanyDetailByIdResponse>
    {
        private readonly IReadRepositoryManager _repo;

        public GetCompanyByIdQueryHandler(IReadRepositoryManager repo)
            => _repo = repo;

        public async Task<Result<GetCompanyDetailByIdResponse>> Handle
        (
            GetCompanyByIdRequest request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                Result<GetCompanyDetailByIdResponse> result = await _repo.CompanyReadRepository.GetCompanyDetailsById(request.CompanyID);

                if (result.IsFailure)
                {
                    return Result<GetCompanyDetailByIdResponse>.Failure<GetCompanyDetailByIdResponse>(
                        new Error("GetCompanyByIdQueryHandler.Handle.Handle", result.Error.Message)
                    );
                }

                return result.Value;

            }
            catch (Exception ex)
            {
                return Result<GetCompanyDetailByIdResponse>.Failure<GetCompanyDetailByIdResponse>(
                    new Error("GetCompanyByIdQueryHandler.Handle", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}