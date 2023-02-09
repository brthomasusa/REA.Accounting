#pragma warning disable CS8603

using FluentValidation;
using MediatR;
using System.Text;
using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Server
{
    public class FluentValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse> where TResponse : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public FluentValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
            => _validators = validators;

        public async Task<TResponse> Handle
        (
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next
        )
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors)
                                                .Where(f => f != null)
                                                .ToList();

                if (failures.Count != 0)
                {
                    StringBuilder sb = new();
                    failures.ToList().ForEach(err => sb.AppendLine(err.ErrorMessage));

                    return OperationResult<int>.CreateFailure(sb.ToString()) as TResponse;
                }
            }

            return await next();
        }
    }
}