using MediatR;
using Movies.Domain.MovieAggregate;

namespace Movies.Application.Features.Movies.Queries;

public class SearchMoviesQueryHandler(IMovieRepository movieRepository)
	: IRequestHandler<SearchMoviesQuery, List<MovieResponse>>
{
	public async Task<List<MovieResponse>> Handle(SearchMoviesQuery request, CancellationToken cancellationToken)
	{
		var test = await movieRepository.GetAll();
		return test.Select(x => x.ToResponse()).ToList();
	}
}