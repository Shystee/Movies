namespace Movies.Application;

public sealed record GetGenresQuery : IQuery<List<GenreResponse>>;