#pragma warning disable CS8603

using MediatR;
using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.Behaviors
{
    public class BusinessRulesValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<int>, IRequest<TResponse>
        where TResponse : Result
    {
        private readonly CommandValidator<TRequest> _businessRulesValidator;

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
    }
}