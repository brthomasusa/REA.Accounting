using MediatR;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.Interfaces.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}