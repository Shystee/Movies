using ErrorOr;

namespace Movies.Application;

internal static class ApplicationErrors
{
	public static class Movie
	{
		public static Error MovieNotFound => Error.NotFound("Movie.NotFound", "Movie not found");
	}
}