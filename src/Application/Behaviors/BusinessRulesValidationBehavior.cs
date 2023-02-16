#pragma warning disable CS8603

using MediatR;
using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.Behaviors
{
    public class BusinessRulesValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICommand
        where TResponse : Result
    {
        private CommandValidator<TRequest> _businessRulesValidator;

        public BusinessRulesValidationBehavior(CommandValidator<TRequest> rulesValidator)
            => _businessRulesValidator = rulesValidator;

        public async Task<TResponse> Handle
        (
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next
        )
        {
            var isCommand = typeof(TRequest).GetInterfaces()
                                            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommand<>));

            if (isCommand)
            {
                OperationResult<bool> result = await _businessRulesValidator.Validate(request);

                if (result.Success)
                {
                    return await next();
                }
                else
                {
                    return (Result<int>.Failure<int>(new Error("BusinessRulesValidationBehavior.Handle", result.NonSuccessMessage!))) as TResponse;
                }
            }

            return await next();
        }
    }
}