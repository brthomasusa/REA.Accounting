using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.DeleteEmployee
{
    public sealed class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand, OperationResult<bool>>
    {
        private IWriteRepositoryManager _repo;

        public DeleteEmployeeCommandHandler(IWriteRepositoryManager repo)
            => _repo = repo;

        public async Task<OperationResult<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                OperationResult<Employee> getResult = await _repo.EmployeeAggregate.GetByIdAsync(request.EmployeeID);

                if (getResult.Success)
                {
                    OperationResult<bool> deleteDbResult = await _repo.EmployeeAggregate.Delete(getResult.Result);
                    if (deleteDbResult.Success)
                        return OperationResult<bool>.CreateSuccessResult(true);

                    return OperationResult<bool>.CreateFailure(deleteDbResult.NonSuccessMessage!);
                }
                else
                {
                    return OperationResult<bool>.CreateFailure(getResult.NonSuccessMessage!);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }
    }
}