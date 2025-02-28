using Dapper;

namespace Movies.Application;

internal sealed class GetMoviesByGenreQueryHandler(IDatabaseConnectionFactory connectionFactory)
	: IQueryHandler<GetMoviesByGenreQuery, List<MovieResponse>>
{
	public async Task<List<MovieResponse>> Handle(GetMoviesByGenreQuery request, CancellationToken cancellationToken)
	{
		const string sql = """
		                   SELECT 
		                       m.id AS Id, 
		                       m.title AS Title, 
		                       m.description AS Description
		                   FROM 
		                       movie m
		                   JOIN 
		                       movie_genre mg ON m.id = mg.movie_id
		                   WHERE 
		                       mg.genre_id = @GenreId
		                       AND m.is_active = true
		                   ORDER BY 
		                       m.title
		                   """;

		using var connection = connectionFactory.CreateConnection();
		var movies = await connection.QueryAsync<MovieResponse>(sql, new
		{
			request.GenreId
		});

		return movies.ToList();
	}
}