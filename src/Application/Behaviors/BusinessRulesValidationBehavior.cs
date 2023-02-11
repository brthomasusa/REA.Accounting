#pragma warning disable CS8603

using MediatR;
using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.SharedKernel.Exceptions;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Server.Behaviors
{
    public class BusinessRulesValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse> where TResponse : class
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
            OperationResult<bool> result = await _businessRulesValidator.Validate(request);

            if (result.Success)
            {
                return await next();
            }

            return OperationResult<int>.CreateFailure(result.NonSuccessMessage!) as TResponse;
        }
    }
}