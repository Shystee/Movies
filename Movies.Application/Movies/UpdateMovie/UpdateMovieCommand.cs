using ErrorOr;
using Movies.Application.Movies.GetMovies;

namespace Movies.Application.Movies.UpdateMovie;

public sealed record UpdateMovieCommand(Guid Id, string Title, string Description, int Rating, int ReleaseYear)
	: ICommand<ErrorOr<MovieResponse>>;