using MediatR;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.Interfaces.Messaging
{
    public interface ICommandValidator<TCommand> : IRequest<TCommand>
    {
        Task<Result> Validate(TCommand command);

        void SetNext(ICommandValidator<TCommand> next);
    }
}