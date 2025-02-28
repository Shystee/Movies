using ErrorOr;
using Movies.Application.Movies.GetMovies;
using Movies.Domain;

namespace Movies.Application.Movies.UpdateMovie;

public class UpdateMovieCommandHandler(IMovieRepository movieRepository) : ICommandHandler<UpdateMovieCommand, ErrorOr<MovieResponse>>
{
	public Task<ErrorOr<MovieResponse>> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
	{
		return Task.FromResult<ErrorOr<MovieResponse>>(new MovieResponse(Guid.NewGuid(), "Test", "Description"));

		// var movieIdResult = MovieId.Create(request.Id);
		// var titleResult = Title.Create(request.Title);
		// var descriptionResult = Description.Create(request.Description);
		// var ratingResult = Rating.Create(request.Rating);
		// var releaseYearResult = ReleaseYear.Create(request.ReleaseYear);
		// var errors = ErrorOrCombineExtensions.CombineErrors(movieIdResult, titleResult, descriptionResult, ratingResult, releaseYearResult);
		// if (errors.Count > 0)
		// {
		// 	return errors;
		// }
		//
		// var movie = await movieRepository.GetById(movieIdResult.Value);
		// if (movie is null)
		// {
		// 	return ApplicationErrors.Movie.MovieNotFound;
		// }
		//
		// movie.Update(titleResult.Value, descriptionResult.Value, ratingResult.Value, releaseYearResult.Value);
		// return movie.ToResponse();
	}
}