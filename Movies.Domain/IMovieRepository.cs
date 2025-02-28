using Movies.Domain.MovieAggregate;
using Movies.Domain.MovieAggregate.ValueObjects;

namespace Movies.Domain;

public interface IMovieRepository
{
	Task Add(Movie movie);

	Task<List<Movie>> GetAll();

	Task<Movie?> GetById(MovieId id);

	void Remove(Movie movie);
}