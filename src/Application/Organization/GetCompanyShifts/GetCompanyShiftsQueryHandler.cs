using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.Organization.GetCompanyShifts
{
    public class GetCompanyShiftsQueryHandler : IQueryHandler<GetCompanyShiftsRequest, PagedList<GetCompanyShiftsResponse>>
    {
        private readonly IReadRepositoryManager _repo;

        public GetCompanyShiftsQueryHandler(IReadRepositoryManager repo)
            => _repo = repo;

        public async Task<Result<PagedList<GetCompanyShiftsResponse>>> Handle
        (
            GetCompanyShiftsRequest request,
            CancellationToken cancellationToken
        )
        {
            try
            {

                Result<PagedList<GetCompanyShiftsResponse>> result =
                    await _repo.CompanyReadRepository.GetCompanyShifts(request.PagingParameters);

                if (result.IsFailure)
                {
                    return Result<PagedList<GetCompanyShiftsResponse>>.Failure<PagedList<GetCompanyShiftsResponse>>(
                        new Error("GetCompanyShiftsQueryHandler.Handle", result.Error.Message)
                    );
                }

                return result.Value;

            }
            catch (Exception ex)
            {
                return Result<PagedList<GetCompanyShiftsResponse>>.Failure<PagedList<GetCompanyShiftsResponse>>(
                    new Error("GetCompanyShiftsQueryHandler.Handle", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}