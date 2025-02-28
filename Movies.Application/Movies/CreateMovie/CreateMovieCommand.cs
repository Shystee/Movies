using ErrorOr;
using Movies.Application.Movies.GetMovies;

namespace Movies.Application.Movies.CreateMovie;

public sealed record CreateMovieCommand(string Title, string Description, int ReleaseYear, int Rating) : ICommand<ErrorOr<MovieResponse>>;