using ErrorOr;

namespace Movies.Domain.MovieAggregate.ValueObjects;

public sealed record Rating
{
	public const float MaxRating = 10.0f;
	public const int MinCount = 0;
	public const float MinRating = 0.0f;

	private Rating(float averageRating, int ratingCount)
	{
		AverageRating = averageRating;
		RatingCount = ratingCount;
	}

	public float AverageRating { get; }

	public int RatingCount { get; }

	public static ErrorOr<Rating> Create(float averageRating, int ratingCount)
	{
		var ratingResult = averageRating.ToErrorOr()
			.FailIf(val => val is < MinRating or > MaxRating, DomainErrors.Movie.Rating.InvalidAverage);

		var countResult = ratingCount.ToErrorOr()
			.FailIf(val => val < MinCount, DomainErrors.Movie.Rating.InvalidCount);

		var errors = ErrorOrCombineExtensions.CombineErrors(ratingResult, countResult);
		if (errors.Count > 0)
		{
			return errors;
		}

		return new Rating(averageRating, ratingCount);
	}

	public static Rating CreateNew() => new(0.0f, 0);
}