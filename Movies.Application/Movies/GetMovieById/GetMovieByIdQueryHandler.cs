using ErrorOr;
using Movies.Application.Movies.GetMovies;
using Movies.Domain;
using Movies.Domain.MovieAggregate.ValueObjects;

namespace Movies.Application.Movies.GetMovieById;

internal sealed class GetMovieByIdQueryHandler(IMovieRepository movieRepository)
	: IQueryHandler<GetMovieByIdQuery, ErrorOr<MovieResponse>>
{
	public async Task<ErrorOr<MovieResponse>> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
	{
		var id = MovieId.Create(request.Id);
		if (id.IsError)
		{
			return id.Errors;
		}
		
		var movie = await movieRepository.GetById(id.Value);
		if (movie is null)
		{
			return ApplicationErrors.Movie.MovieNotFound;
		}

		return movie.ToResponse();
	}
}