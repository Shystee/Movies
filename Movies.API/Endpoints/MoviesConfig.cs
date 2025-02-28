using Asp.Versioning;
using MediatR;
using Movies.Application;
using Movies.Application.Movies.DeleteMovie;
using Movies.Application.Movies.GetMovieById;
using Movies.Application.Movies.GetMovies;

namespace Movies.API.Endpoints;

internal static class IdentityConfiguration
{
	public const string IdentityPrefixUri = "api/v{version:apiVersion}";

	public static ApiVersion Default { get; } = new(1, 0);
}

internal static class MoviesConfig
{
	private const string GetMovieRouteName = "GetMovieById";
	private const string MoviesPrefixUri = $"{IdentityConfiguration.IdentityPrefixUri}/movies";

	internal static IEndpointRouteBuilder MapMoviesEndpoints(this IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.NewVersionedApi()
			.MapGroup(MoviesPrefixUri)
			.HasApiVersion(IdentityConfiguration.Default)
			.WithTags("Movies");

		group.MapGet("", GetMovies)
			.WithName("GetMovies")
			.WithSummary("Get all movies")
			.WithOpenApi();

		group.MapGet("/{id:guid}", GetMovie)
			.WithName(GetMovieRouteName)
			.WithSummary("Get a movie by ID")
			.WithOpenApi();

		group.MapPost("", CreateMovie)
			.WithName("CreateMovie")
			.WithSummary("Create a new movie")
			.WithOpenApi();

		group.MapDelete("/{id:guid}", DeleteMovie)
			.WithName("DeleteMovie")
			.WithSummary("Delete a movie")
			.WithOpenApi();

		group.MapPut("/{id:guid}", EditMovie)
			.WithName("EditMovie")
			.WithSummary("Update a movie")
			.WithOpenApi();

		return endpoints;
	}

	private static async Task<IResult> CreateMovie(CreateMovieRequest request, ISender sender)
	{
		return await sender.Send(request.ToCommand())
			.ToResult(movie => Results.CreatedAtRoute(GetMovieRouteName, new
			{
				id = movie.Id
			}, movie));
	}

	private static async Task<IResult> DeleteMovie(Guid id, ISender sender)
	{
		return await sender.Send(new DeleteMovieCommand(id)).ToResult(_ => Results.NoContent());
	}

	private static IResult EditMovie(Guid id)
	{
		return Results.Ok();
	}

	private static async Task<IResult> GetMovie(Guid id, ISender sender)
	{
		return await sender.Send(new GetMovieByIdQuery(id)).ToResult(Results.Ok);
	}

	private static async Task<IResult> GetMovies(ISender sender)
	{
		var result = await sender.Send(new GetMoviesQuery());
		return Results.Ok(result);
	}
}