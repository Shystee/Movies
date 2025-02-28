using Dapper;

namespace Movies.Application;

internal sealed class GetGenresQueryHandler(IDatabaseConnectionFactory connectionFactory)
	: IQueryHandler<GetGenresQuery, List<GenreResponse>>
{
	public async Task<List<GenreResponse>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
	{
		const string sql = "SELECT g.id AS Id, g.name AS Name, g.description AS Description FROM genre g ORDER BY g.name";
		using var connection = connectionFactory.CreateConnection();
		var genres = await connection.QueryAsync<GenreResponse>(sql);
		return genres.ToList();
	}
}