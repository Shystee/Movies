using MediatR;
using Movies.Application.Features.Movies;
using Movies.Application.Features.Movies.Queries;

namespace Movies.API.Endpoints;

public static class MovieEndpoints
{
	public static IEndpointRouteBuilder MapMovieEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup($"{IdentityConfiguration.IdentityPrefixUri}/movies")
			.WithApiVersionSet(app.NewApiVersionSet()
				.HasApiVersion(IdentityConfiguration.Default)
				.ReportApiVersions()
				.Build())
			.MapToApiVersion(IdentityConfiguration.Default);

		group.MapGet("/", GetMovies)
			.WithName("SearchMovies")
			.WithSummary("Search movies by title")
			.WithDescription("Returns a list of movies matching the search term")
			.Produces<List<MovieResponse>>();

		return app;
	}

	private static async Task<IResult> GetMovies(string? title, ISender sender, CancellationToken cancellationToken)
	{
		var query = new SearchMoviesQuery(title);
		var result = await sender.Send(query, cancellationToken);
		return Results.Ok(result);
	}
}