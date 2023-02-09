using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.DeleteEmployee
{
    public sealed class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand, OperationResult<int>>
    {
        private IWriteRepositoryManager _repo;

        public DeleteEmployeeCommandHandler(IWriteRepositoryManager repo)
            => _repo = repo;

        public async Task<OperationResult<int>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                OperationResult<Employee> getResult = await _repo.EmployeeAggregate.GetByIdAsync(request.EmployeeID);

                if (getResult.Success)
                {
                    OperationResult<int> deleteDbResult = await _repo.EmployeeAggregate.Delete(getResult.Result);
                    if (deleteDbResult.Success)
                        return OperationResult<int>.CreateSuccessResult(0);

                    return OperationResult<int>.CreateFailure(deleteDbResult.NonSuccessMessage!);
                }
                else
                {
                    return OperationResult<int>.CreateFailure(getResult.NonSuccessMessage!);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<int>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }
    }
}