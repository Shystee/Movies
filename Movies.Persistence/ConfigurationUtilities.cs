using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain;

namespace Movies.Persistence;

public static class ConfigurationUtilities
{
	public static EntityTypeBuilder<TEntity> ConfigureAuditableEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
		where TEntity : class, IAuditable
	{
		builder.Property(m => m.CreatedAt)
			.HasColumnName("created_at")
			.HasColumnType("timestamptz")
			.IsRequired();

		builder.Property(m => m.UpdatedAt)
			.HasColumnName("updated_at")
			.HasColumnType("timestamptz")
			.IsRequired();

		return builder;
	}
}