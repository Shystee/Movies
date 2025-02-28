using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain;

namespace Movies.Persistence;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
	public void Configure(EntityTypeBuilder<Person> builder)
	{
		builder.ToTable("person");

		builder.HasKey(p => p.Id);

		builder.Property(p => p.Id)
			.HasColumnName("id")
			.HasColumnType("uuid")
			.ValueGeneratedNever()
			.HasConversion(
				id => id.Value,
				value => PersonId.Create(value).Value);

		builder.Property(p => p.CreatedAt)
			.HasColumnName("created_at")
			.HasColumnType("timestamp with time zone")
			.IsRequired();

		builder.Property(p => p.UpdatedAt)
			.HasColumnName("updated_at")
			.HasColumnType("timestamp with time zone")
			.IsRequired();

		builder.OwnsOne(p => p.Name, name =>
		{
			name.Property(n => n.Value)
				.HasColumnName("name")
				.HasColumnType("varchar")
				.HasMaxLength(PersonName.MaxLength)
				.IsRequired();
		});

		builder.OwnsOne(p => p.PhotoUrl, url =>
		{
			url.Property(u => u.Value)
				.HasColumnName("photo_url")
				.HasColumnType("varchar")
				.HasMaxLength(Url.MaxLength);
		});

		builder.OwnsOne(p => p.BirthDate, birthDate =>
		{
			birthDate.Property(bd => bd.Value)
				.HasColumnName("birth_date")
				.HasColumnType("date");
		});

		builder.OwnsOne(p => p.Biography, biography =>
		{
			biography.Property(b => b.Value)
				.HasColumnName("biography")
				.HasColumnType("text");
		});
	}
}