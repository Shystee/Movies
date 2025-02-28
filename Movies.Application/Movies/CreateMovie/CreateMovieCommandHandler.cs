using ErrorOr;
using JetBrains.Annotations;
using Movies.Application.Movies.GetMovies;
using Movies.Domain;

namespace Movies.Application.Movies.CreateMovie;

[UsedImplicitly]
internal sealed class CreateMovieCommandHandler(IMovieRepository movieRepository)
	: ICommandHandler<CreateMovieCommand, ErrorOr<MovieResponse>>
{
	public Task<ErrorOr<MovieResponse>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
	{
		return Task.FromResult<ErrorOr<MovieResponse>>(new MovieResponse(Guid.NewGuid(), "Test", "Description"));
		
		// var title = Title.Create(request.Title);
		// var description = Description.Create(request.Description);
		// var rating = Rating.Create(request.Rating);
		// var releaseYear = ReleaseYear.Create(request.ReleaseYear);
		// var errors = ErrorOrCombineExtensions.CombineErrors(title, description, rating, releaseYear);
		// if (errors.Count > 0)
		// {
		// 	return errors;
		// }
		//
		// var movie = Movie.Create(title.Value, description.Value, rating.Value, releaseYear.Value);
		// await movieRepository.Add(movie);
		// return movie.ToResponse();
	}
}