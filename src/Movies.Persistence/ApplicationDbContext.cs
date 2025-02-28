using Microsoft.EntityFrameworkCore;

namespace Movies.Persistence;

public sealed class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
	protected override void OnModelCreating(ModelBuilder modelBuilder) =>
		modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
}