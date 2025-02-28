namespace Movies.Application;

public sealed record MovieResponse(
	Guid Id,
	string Title,
	string Description);