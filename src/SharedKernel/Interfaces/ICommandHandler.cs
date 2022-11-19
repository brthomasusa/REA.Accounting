using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.SharedKernel.Interfaces
{
    public interface ICommandHandler<TCommand>
    {
        Task<OperationResult<bool>> Handle(TCommand command);

        void SetNext(ICommandHandler<TCommand> next);
    }
}