using Movies.Application.Movies.CreateMovie;
using Movies.Domain.MovieAggregate;

namespace Movies.Application;

public static class MovieMapping
{
	public static CreateMovieCommand ToCommand(this CreateMovieRequest request) =>
		new(request.Title, request.Description, request.ReleaseYear, request.Rating);

	public static MovieResponse ToResponse(this Movie movie) => new(movie.Id.Value, movie.Title.Value, movie.Description.Value);
}