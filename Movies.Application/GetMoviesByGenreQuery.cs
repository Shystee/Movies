namespace Movies.Application;

public sealed record GetMoviesByGenreQuery(Guid GenreId) : IQuery<List<MovieResponse>>;