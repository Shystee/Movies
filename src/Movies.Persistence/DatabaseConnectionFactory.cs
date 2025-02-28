using Microsoft.Extensions.Configuration;
using Movies.Application;
using Npgsql;
using System.Data;

namespace Movies.Persistence;

public class PostgresConnectionFactory(IConfiguration configuration) : IDatabaseConnectionFactory
{
	private readonly string _connectionString = configuration.GetConnectionString("Database") ??
	                                            throw new ArgumentNullException(nameof(configuration),
		                                            "Database connection string not found in configuration.");

	public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}