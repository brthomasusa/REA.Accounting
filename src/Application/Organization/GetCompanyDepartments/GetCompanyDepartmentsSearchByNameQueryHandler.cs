using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.Organization.GetCompanyDepartments // GetCompanyDepartmentsSearchByNameRequest
{
    public sealed class GetCompanyDepartmentsSearchByNameQueryHandler : IQueryHandler<GetCompanyDepartmentsSearchByNameRequest, PagedList<GetCompanyDepartmentsResponse>>
    {
        private readonly IReadRepositoryManager _repo;

        public GetCompanyDepartmentsSearchByNameQueryHandler(IReadRepositoryManager repo)
            => _repo = repo;

        public async Task<Result<PagedList<GetCompanyDepartmentsResponse>>> Handle
        (
            GetCompanyDepartmentsSearchByNameRequest request,
            CancellationToken cancellationToken
        )
        {
            try
            {

                Result<PagedList<GetCompanyDepartmentsResponse>> result =
                    await _repo.CompanyReadRepository.GetCompanyDepartmentsSearchByName(request.DepartmentName, request.PagingParameters);

                if (result.IsFailure)
                {
                    return Result<PagedList<GetCompanyDepartmentsResponse>>.Failure<PagedList<GetCompanyDepartmentsResponse>>(
                        new Error("GetCompanyDepartmentsSearchByNameQueryHandler.Handle", result.Error.Message)
                    );
                }

                return result.Value;

            }
            catch (Exception ex)
            {
                return Result<PagedList<GetCompanyDepartmentsResponse>>.Failure<PagedList<GetCompanyDepartmentsResponse>>(
                    new Error("GetCompanyDepartmentsSearchByNameQueryHandler.Handle", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}