namespace Movies.Application;

public sealed record CreateMovieRequest(string Title, string Description, int ReleaseYear, int Rating);