using Movies.Application.Features.Movies;
using Movies.Application.Features.Movies.Queries;
using Movies.Domain.MovieAggregate;

namespace Movies.Application;

public static class MovieMapping
{
	public static MovieResponse ToResponse(this Movie movie) => new(movie.Id.Value, movie.Title.Value, movie.Description.Value);
}