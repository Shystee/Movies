using Dapper;

namespace Movies.Application;

internal sealed class GetTrendingGenresQueryHandler(IDatabaseConnectionFactory connectionFactory)
	: IQueryHandler<GetTrendingGenresQuery, List<TrendingGenreResponse>>
{
	public async Task<List<TrendingGenreResponse>> Handle(GetTrendingGenresQuery request, CancellationToken cancellationToken)
	{
		const string sql = """
		                   SELECT 
		                       g.id AS Id, 
		                       g.name AS Name, 
		                       g.description AS Description,
		                       COUNT(mg.movie_id)::INTEGER AS MovieCount,
		                       COALESCE(AVG(m.average_rating), 0) AS AverageRating
		                   FROM 
		                       genre g
		                   LEFT JOIN 
		                       movie_genre mg ON g.id = mg.genre_id
		                   LEFT JOIN 
		                       movie m ON mg.movie_id = m.id AND m.is_active = true
		                   GROUP BY 
		                       g.id, g.name, g.description
		                   ORDER BY 
		                       COUNT(mg.movie_id) DESC, AVG(m.average_rating) DESC NULLS LAST
		                   LIMIT 10
		                   """;

		using var connection = connectionFactory.CreateConnection();
		var trendingGenres = await connection.QueryAsync<TrendingGenreResponse>(sql);
		return trendingGenres.ToList();
	}
}