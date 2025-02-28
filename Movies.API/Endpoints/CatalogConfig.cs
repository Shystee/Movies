using MediatR;
using Movies.Application;

namespace Movies.API.Endpoints;

internal static class CatalogConfig
{
	private const string CatalogPrefixUri = $"{IdentityConfiguration.IdentityPrefixUri}/catalog";

	internal static IEndpointRouteBuilder MapCatalogEndpoints(this IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.NewVersionedApi()
			.MapGroup(CatalogPrefixUri)
			.HasApiVersion(IdentityConfiguration.Default)
			.WithTags("Catalog");

		group.MapGet("/genres", GetGenres)
			.WithName("GetGenres")
			.WithSummary("Get all genres")
			.WithOpenApi();

		group.MapGet("/genres/trending", GetTrendingGenres)
			.WithName("GetTrendingGenres")
			.WithSummary("Get trending genres based on movie count and ratings")
			.WithOpenApi();

		group.MapGet("/movies/by-genre/{genreId:guid}", GetMoviesByGenre)
			.WithName("GetMoviesByGenre")
			.WithSummary("Get movies by genre ID")
			.WithOpenApi();

		return endpoints;
	}

	private static async Task<IResult> GetGenres(ISender sender)
	{
		var result = await sender.Send(new GetGenresQuery());
		return Results.Ok(result);
	}

	private static async Task<IResult> GetTrendingGenres(ISender sender)
	{
		var result = await sender.Send(new GetTrendingGenresQuery());
		return Results.Ok(result);
	}

	private static async Task<IResult> GetMoviesByGenre(Guid genreId, ISender sender)
	{
		var result = await sender.Send(new GetMoviesByGenreQuery(genreId));
		return Results.Ok(result);
	}
}