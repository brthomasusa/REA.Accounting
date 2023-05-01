using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Shared.Models.HumanResources;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.GetEmployeeDetailsById
{
    public sealed class GetEmployeeDetailsByIdWithAllInfoQueryHandler : IQueryHandler<GetEmployeeDetailsByIdWithAllInfoRequest, EmployeeDetailReadModel>
    {
        private readonly IReadRepositoryManager _repo;

        public GetEmployeeDetailsByIdWithAllInfoQueryHandler(IReadRepositoryManager repo)
            => _repo = repo;

        public async Task<Result<EmployeeDetailReadModel>> Handle
        (
            GetEmployeeDetailsByIdWithAllInfoRequest request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                Result<EmployeeDetailReadModel> result =
                    await _repo.EmployeeReadRepository.GetEmployeeDetailsByIdWithAllInfo(request.EmployeeID);

                if (result.IsFailure)
                {
                    return Result<EmployeeDetailReadModel>.Failure<EmployeeDetailReadModel>(
                        new Error("GetEmployeeDetailsByIdWithAllInfoQueryHandler.Handle", result.Error.Message)
                    );
                }

                return result.Value;

            }
            catch (Exception ex)
            {
                return Result<EmployeeDetailReadModel>.Failure<EmployeeDetailReadModel>(
                    new Error("GetEmployeeDetailsByIdWithAllInfoQueryHandler.Handle", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}