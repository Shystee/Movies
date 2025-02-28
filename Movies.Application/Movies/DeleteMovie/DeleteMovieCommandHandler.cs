using ErrorOr;
using Movies.Domain;
using Movies.Domain.MovieAggregate.ValueObjects;

namespace Movies.Application.Movies.DeleteMovie;

public class DeleteMovieCommandHandler(IMovieRepository movieRepository) : ICommandHandler<DeleteMovieCommand, ErrorOr<Deleted>>
{
	public async Task<ErrorOr<Deleted>> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
	{
		var movieId = MovieId.Create(request.Id);
		if (movieId.IsError)
		{
			return movieId.Errors;
		}
		
		var movie = await movieRepository.GetById(movieId.Value);
		if (movie is null)
		{
			return ApplicationErrors.Movie.MovieNotFound;
		}
    
		movieRepository.Remove(movie);
		return Result.Deleted;
	}
}