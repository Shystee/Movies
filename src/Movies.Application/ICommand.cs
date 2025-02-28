using MediatR;

namespace Movies.Application;

public interface ICommand<out TResponse> : IRequest<TResponse>;

public interface ICommand : IRequest;