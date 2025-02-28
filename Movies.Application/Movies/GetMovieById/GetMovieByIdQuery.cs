using ErrorOr;
using Movies.Application.Movies.GetMovies;

namespace Movies.Application.Movies.GetMovieById;

public sealed record GetMovieByIdQuery(Guid Id) : IQuery<ErrorOr<MovieResponse>>;