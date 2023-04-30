using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Queries.HumanResources;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.GetEmployeeDetailsById
{
    public sealed class GetEmployeeListItemsQueryHandler : IQueryHandler<GetEmployeeListItemsRequest, PagedList<GetEmployeeListItemsResponse>>
    {
        private readonly IReadRepositoryManager _repo;

        public GetEmployeeListItemsQueryHandler(IReadRepositoryManager repo)
            => _repo = repo;

        public async Task<Result<PagedList<GetEmployeeListItemsResponse>>> Handle
        (
            GetEmployeeListItemsRequest request,
            CancellationToken cancellationToken
        )
        {
            try
            {

                Result<PagedList<GetEmployeeListItemsResponse>> result =
                    await _repo.EmployeeReadRepository.GetEmployeeListItemsSearchByLastName(request.LastName, request.PagingParameters);

                if (result.IsFailure)
                {
                    return Result<PagedList<GetEmployeeListItemsResponse>>.Failure<PagedList<GetEmployeeListItemsResponse>>(
                        new Error("GGetEmployeeListItemsQueryHandler.Handle", result.Error.Message)
                    );
                }

                return result.Value;

            }
            catch (Exception ex)
            {
                return Result<PagedList<GetEmployeeListItemsResponse>>.Failure<PagedList<GetEmployeeListItemsResponse>>(
                    new Error("GetEmployeeListItemsQueryHandler.Handle", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}