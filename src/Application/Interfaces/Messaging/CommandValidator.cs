using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.Interfaces.Messaging
{
    public abstract class CommandValidator<TCommand> : ICommandValidator<TCommand>
    {
        protected ICommandValidator<TCommand>? Next { get; private set; }

        public void SetNext(ICommandValidator<TCommand> next)
        {
            Next = next;
        }

        public virtual async Task<OperationResult<bool>> Validate(TCommand command)
        {
            if (Next is not null)
            {
                await Next.Validate(command);
            }

            return OperationResult<bool>.CreateSuccessResult(true);
        }
    }
}