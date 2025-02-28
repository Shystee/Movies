using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.MovieAggregate;

namespace Movies.Persistence;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
	public void Configure(EntityTypeBuilder<Genre> builder)
	{
		builder.ToTable(TableNames.Genres);
		builder.HasKey(g => g.Id);
		builder.Property(g => g.Id)
			.HasColumnName("id")
			.HasColumnType("uuid")
			.ValueGeneratedNever()
			.HasConversion(
				id => id.Value,
				value => GenreId.Create(value).Value);

		builder.OwnsOne(g => g.Name, name =>
		{
			name.Property(n => n.Value)
				.HasColumnName("name")
				.HasColumnType("varchar")
				.HasMaxLength(GenreName.MaxLength)
				.IsRequired();
		});

		builder.OwnsOne(g => g.Description, description =>
		{
			description.Property(d => d.Value)
				.HasColumnName("description")
				.HasColumnType("text")
				.IsRequired();
		});
	}
}