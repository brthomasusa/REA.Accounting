using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.Shared.Models.HumanResources;

namespace REA.Accounting.Application.HumanResources.GetEmployeeDetailsById
{
    public sealed class GetEmployeeListItemsQueryHandler : IQueryHandler<GetEmployeeListItemsRequest, PagedList<EmployeeListItemReadModel>>
    {
        private readonly IReadRepositoryManager _repo;

        public GetEmployeeListItemsQueryHandler(IReadRepositoryManager repo)
            => _repo = repo;

        public async Task<Result<PagedList<EmployeeListItemReadModel>>> Handle
        (
            GetEmployeeListItemsRequest request,
            CancellationToken cancellationToken
        )
        {
            try
            {

                Result<PagedList<EmployeeListItemReadModel>> result =
                    await _repo.EmployeeReadRepository.GetEmployeeListItemsSearchByLastName(request.LastName, request.PagingParameters);

                if (result.IsFailure)
                {
                    return Result<PagedList<EmployeeListItemReadModel>>.Failure<PagedList<EmployeeListItemReadModel>>(
                        new Error("GGetEmployeeListItemsQueryHandler.Handle", result.Error.Message)
                    );
                }

                return result.Value;

            }
            catch (Exception ex)
            {
                return Result<PagedList<EmployeeListItemReadModel>>.Failure<PagedList<EmployeeListItemReadModel>>(
                    new Error("GetEmployeeListItemsQueryHandler.Handle", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}