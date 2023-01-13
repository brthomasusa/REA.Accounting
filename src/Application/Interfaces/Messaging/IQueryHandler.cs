﻿using MediatR;

namespace REA.Accounting.Application.Interfaces.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}