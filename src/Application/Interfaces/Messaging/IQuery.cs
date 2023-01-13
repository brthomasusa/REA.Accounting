using MediatR;

namespace REA.Accounting.Application.Interfaces.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}