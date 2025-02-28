using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class PersonId : AggregateRootId<Guid>
{
	private PersonId(Guid id)
		: base(id)
	{
	}

	[UsedImplicitly]
	private PersonId()
	{
	}

	public static ErrorOr<PersonId> Create(Guid value)
	{
		if (value == Guid.Empty)
		{
			return DomainErrors.Person.InvalidPersonId;
		}

		return new PersonId(value);
	}

	public static PersonId CreateUnique() => new(Guid.NewGuid());
}