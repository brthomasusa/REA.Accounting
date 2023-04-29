using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Queries.HumanResources;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.GetEmployeeDetailsById
{
    public sealed class GetEmployeeDetailsByIdWithAllInfoQueryHandler : IQueryHandler<GetEmployeeDetailsByIdWithAllInfoRequest, GetEmployeeDetailsByIdWithAllInfoResponse>
    {
        private readonly IReadRepositoryManager _repo;

        public GetEmployeeDetailsByIdWithAllInfoQueryHandler(IReadRepositoryManager repo)
            => _repo = repo;

        public async Task<Result<GetEmployeeDetailsByIdWithAllInfoResponse>> Handle
        (
            GetEmployeeDetailsByIdWithAllInfoRequest request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                Result<GetEmployeeDetailsByIdWithAllInfoResponse> result =
                    await _repo.EmployeeReadRepository.GetEmployeeDetailsByIdWithAllInfo(request.EmployeeID);

                if (result.IsFailure)
                {
                    return Result<GetEmployeeDetailsByIdWithAllInfoResponse>.Failure<GetEmployeeDetailsByIdWithAllInfoResponse>(
                        new Error("GetEmployeeDetailsByIdWithAllInfoQueryHandler.Handle", result.Error.Message)
                    );
                }

                return result.Value;

            }
            catch (Exception ex)
            {
                return Result<GetEmployeeDetailsByIdWithAllInfoResponse>.Failure<GetEmployeeDetailsByIdWithAllInfoResponse>(
                    new Error("GetEmployeeDetailsByIdWithAllInfoQueryHandler.Handle", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}