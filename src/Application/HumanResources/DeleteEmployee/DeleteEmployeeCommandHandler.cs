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
        private IWriteRepositoryManager _repo;

        public DeleteEmployeeCommandHandler(IWriteRepositoryManager repo)
            => _repo = repo;

        public async Task<Result<int>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                OperationResult<Employee> getResult = await _repo.EmployeeAggregate.GetByIdAsync(request.EmployeeID);

                if (getResult.Success)
                {
                    OperationResult<int> deleteDbResult = await _repo.EmployeeAggregate.Delete(getResult.Result);
                    if (deleteDbResult.Success)
                        return RETURN_VALUE;

                    return Result<int>.Failure<int>(new Error("DeleteEmployeeCommandHandler.Handle", deleteDbResult.NonSuccessMessage!));
                }
                else
                {
                    return Result<int>.Failure<int>(new Error("DeleteEmployeeCommandHandler.Handle", getResult.NonSuccessMessage!));
                }
            }
            catch (Exception ex)
            {
                return Result<int>.Failure<int>(new Error("DeleteEmployeeCommandHandler.Handle", Helpers.GetExceptionMessage(ex)));
            }
        }
    }
}