namespace Movies.Application.Features.Movies;

public sealed record MovieResponse(
	Guid Id,
	string Title,
	string Description);