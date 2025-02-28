using Movies.Domain.MovieAggregate.ValueObjects;

namespace Movies.Domain.MovieAggregate;

public interface IMovieRepository
{
	Task Add(Movie movie);

	Task<List<Movie>> GetAll();

	Task<Movie?> GetById(MovieId id);

	void Remove(Movie movie);
}