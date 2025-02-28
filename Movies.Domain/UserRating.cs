using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class UserRating : Entity<UserRatingId>, IAuditable
{
	public const float MaxRating = 5.0f;
	public const int MaxReviewLength = 1000;
	public const float MinRating = 1.0f;

	private UserRating(
		UserRatingId id,
		Guid contentId,
		ContentType contentType,
		float rating,
		string? review)
		: base(id)
	{
		ContentId = contentId;
		ContentType = contentType;
		Rating = rating;
		Review = review;
		CreatedAt = DateTimeOffset.UtcNow;
		UpdatedAt = DateTimeOffset.UtcNow;
	}

	[UsedImplicitly]
	private UserRating()
	{
	}

	public Guid ContentId { get; private set; }

	public ContentType ContentType { get; private set; }

	public DateTimeOffset CreatedAt { get; set; }

	public float Rating { get; private set; }

	public string? Review { get; private set; }

	public DateTimeOffset UpdatedAt { get; set; }

	public static ErrorOr<UserRating> Create(
		Guid contentId,
		ContentType contentType,
		float rating,
		string? review = null)
	{
		// Validate rating
		if (rating < MinRating || rating > MaxRating)
		{
			return DomainErrors.User.Rating.InvalidValue;
		}

		// Validate review if provided
		if (!string.IsNullOrEmpty(review) && review.Length > MaxReviewLength)
		{
			return DomainErrors.User.Rating.InvalidReview;
		}

		return new UserRating(
			UserRatingId.CreateUnique(),
			contentId,
			contentType,
			rating,
			review);
	}

	public ErrorOr<Updated> UpdateRating(float rating)
	{
		if (rating < MinRating || rating > MaxRating)
		{
			return DomainErrors.User.Rating.InvalidValue;
		}

		Rating = rating;
		UpdatedAt = DateTimeOffset.UtcNow;
		return Result.Updated;
	}

	public ErrorOr<Updated> UpdateReview(string? review)
	{
		if (!string.IsNullOrEmpty(review) && review.Length > MaxReviewLength)
		{
			return DomainErrors.User.Rating.InvalidReview;
		}

		Review = review;
		UpdatedAt = DateTimeOffset.UtcNow;
		return Result.Updated;
	}
}