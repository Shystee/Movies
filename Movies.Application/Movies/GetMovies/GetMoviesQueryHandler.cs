using Movies.Domain;

namespace Movies.Application.Movies.GetMovies;

internal sealed class GetMoviesQueryHandler(IMovieRepository movieRepository) : IQueryHandler<GetMoviesQuery, List<MovieResponse>>
{
	public async Task<List<MovieResponse>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
	{
		var movies = await movieRepository.GetAll();
		return movies.Select(x => x.ToResponse()).ToList();
	}
}