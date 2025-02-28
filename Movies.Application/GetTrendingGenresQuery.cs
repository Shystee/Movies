namespace Movies.Application;

public sealed record GetTrendingGenresQuery : IQuery<List<TrendingGenreResponse>>;