using ErrorOr;

namespace Movies.Application.Movies.DeleteMovie;

public sealed record DeleteMovieCommand(Guid Id) : ICommand<ErrorOr<Deleted>>;