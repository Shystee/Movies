using System.Data;

namespace Movies.Application;

public interface IDatabaseConnectionFactory
{
	IDbConnection CreateConnection();
}