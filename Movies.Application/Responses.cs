namespace Movies.Application;

public sealed record GenreResponse(
	Guid Id,
	string Name,
	string Description);

public sealed record TrendingGenreResponse(
	Guid Id,
	string Name,
	string Description,
	int MovieCount,
	double AverageRating);