namespace Movies.Domain.Common;

public abstract record ValueObject<TValue>(TValue Value) where TValue : notnull
{
	public sealed override string? ToString() => Value.ToString();

	public override int GetHashCode() => Value.GetHashCode();
}