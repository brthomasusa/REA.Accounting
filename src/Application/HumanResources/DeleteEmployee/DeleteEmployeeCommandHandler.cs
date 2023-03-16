using MediatR;
using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.DeleteEmployee
{
    public sealed class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand, int>
    {
        private const int RETURN_VALUE = 0;
        private readonly IWriteRepositoryManager _repo;

        public DeleteEmployeeCommandHandler(IWriteRepositoryManager repo)
            => _repo = repo;

        public async Task<Result<int>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Result<Employee> getResult = await _repo.EmployeeAggregateRepository.GetByIdAsync(request.EmployeeID);

                if (getResult.IsSuccess)
                {
                    Result<int> deleteDbResult = await _repo.EmployeeAggregateRepository.Delete(getResult.Value);

                    if (deleteDbResult.IsSuccess)
                        return RETURN_VALUE;

                    return Result<int>.Failure<int>(new Error("DeleteEmployeeCommandHandler.Handle", deleteDbResult.Error.Message));
                }
                else
                {
                    return Result<int>.Failure<int>(new Error("DeleteEmployeeCommandHandler.Handle", getResult.Error.Message));
                }
            }
            catch (Exception ex)
            {
                return Result<int>.Failure<int>(new Error("DeleteEmployeeCommandHandler.Handle", Helpers.GetExceptionMessage(ex)));
            }
        }
    }
}