using MediatR;

namespace REA.Accounting.Application.Interfaces.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}