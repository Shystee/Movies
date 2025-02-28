using Microsoft.EntityFrameworkCore;
using Movies.Domain;
using Movies.Domain.MovieAggregate;
using Movies.Domain.MovieAggregate.ValueObjects;

namespace Movies.Persistence;

public sealed class MovieRepository(ApplicationDbContext dbContext) : IMovieRepository
{
	public async Task Add(Movie movie) => await dbContext.Set<Movie>().AddAsync(movie);

	public async Task<List<Movie>> GetAll()
	{
		return await dbContext.Set<Movie>()
			.Include(x => x.Genres)
			.AsSplitQuery()
			.ToListAsync();
	}

	public Task<Movie?> GetById(MovieId id) => dbContext.Set<Movie>().FirstOrDefaultAsync(x => x.Id == id);

	public void Remove(Movie movie) => dbContext.Set<Movie>().Remove(movie);
}