using Microsoft.EntityFrameworkCore;

namespace Movies.Persistence;

public sealed class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder) =>
		modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
}