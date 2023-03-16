using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Queries.HumanResources;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.GetEmployeeDetailsById
{
    public sealed class GetEmployeeDetailByIdQueryHandler : IQueryHandler<GetEmployeeDetailByIdRequest, GetEmployeeDetailByIdResponse>
    {
        private readonly IReadRepositoryManager _repo;

        public GetEmployeeDetailByIdQueryHandler(IReadRepositoryManager repo)
            => _repo = repo;

        public async Task<Result<GetEmployeeDetailByIdResponse>> Handle
        (
            GetEmployeeDetailByIdRequest request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                Result<GetEmployeeDetailByIdResponse> result = await _repo.EmployeeReadRepository.GetEmployeeDetailsById(request.EmployeeID);

                if (result.IsFailure)
                {
                    return Result<GetEmployeeDetailByIdResponse>.Failure<GetEmployeeDetailByIdResponse>(
                        new Error("GetEmployeeDetailByIdQueryHandler.Handle", result.Error.Message)
                    );
                }

                return result.Value;

            }
            catch (Exception ex)
            {
                return Result<GetEmployeeDetailByIdResponse>.Failure<GetEmployeeDetailByIdResponse>(
                    new Error("GetEmployeeDetailByIdQueryHandler.Handle", Helpers.GetExceptionMessage(ex))
                );
            }
        }
    }
}