using MediatR;

namespace Movies.Application;

public interface IQuery<out TResponse> : IRequest<TResponse>;