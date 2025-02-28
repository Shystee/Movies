namespace Movies.Domain;

public abstract class ValueObject : IEquatable<ValueObject>
{
	public static bool operator ==(ValueObject? left, ValueObject? right) => Equals(left, right);

	public static bool operator !=(ValueObject? left, ValueObject? right) => !Equals(left, right);

	public bool Equals(ValueObject? other) => other is not null && ValuesAreEqual(other);

	public override bool Equals(object? obj) => obj is ValueObject other && ValuesAreEqual(other);

	public abstract IEnumerable<object?> GetEqualityComponents();

	public override int GetHashCode() => GetEqualityComponents()
		.Aggregate(
			0,
			HashCode.Combine);

	private bool ValuesAreEqual(ValueObject other) => GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
}