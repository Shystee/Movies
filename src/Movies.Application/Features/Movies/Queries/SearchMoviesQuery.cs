namespace Movies.Application.Features.Movies.Queries;

public record SearchMoviesQuery(string? Title) : IQuery<List<MovieResponse>>;