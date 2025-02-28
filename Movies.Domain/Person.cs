using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class Person : Entity<PersonId>, IAuditable
{
	private Person(
		PersonId id,
		PersonName name,
		Url photoUrl,
		BirthDate birthDate,
		Biography biography)
		: base(id)
	{
		Name = name;
		PhotoUrl = photoUrl;
		BirthDate = birthDate;
		Biography = biography;
		CreatedAt = DateTimeOffset.Now;
		UpdatedAt = DateTimeOffset.Now;
	}

	[UsedImplicitly]
	private Person()
	{
	}

	public Biography Biography { get; private set; } = null!;

	public BirthDate BirthDate { get; private set; } = null!;

	public DateTimeOffset CreatedAt { get; set; }

	public PersonName Name { get; private set; } = null!;

	public Url PhotoUrl { get; private set; } = null!;

	public DateTimeOffset UpdatedAt { get; set; }

	public static Person Create(
		PersonName name,
		Url photoUrl,
		BirthDate birthDate,
		Biography biography) => new(
		PersonId.CreateUnique(),
		name,
		photoUrl,
		birthDate,
		biography);
}