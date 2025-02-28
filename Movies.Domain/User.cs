using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class User : AggregateRoot<UserId, Guid>, IAuditable
{
	private readonly List<UserRating> _ratings = new();
	private readonly List<UserWatchlist> _watchlist = new();
	private readonly List<UserWatchProgress> _watchProgress = new();

	private User(
		UserId id,
		Email email,
		PasswordHash passwordHash,
		UserName firstName,
		UserName lastName,
		DateOnly dateOfBirth,
		bool isActive)
		: base(id)
	{
		Email = email;
		PasswordHash = passwordHash;
		FirstName = firstName;
		LastName = lastName;
		DateOfBirth = dateOfBirth;
		LastLoginAt = null;
		IsActive = isActive;
		CreatedAt = DateTimeOffset.UtcNow;
		UpdatedAt = DateTimeOffset.UtcNow;
	}

	[UsedImplicitly]
	private User()
	{
	}

	public DateTimeOffset CreatedAt { get; set; }

	public DateOnly DateOfBirth { get; private set; }

	public Email Email { get; private set; } = null!;

	public UserName FirstName { get; private set; } = null!;

	public bool IsActive { get; private set; }

	public DateTimeOffset? LastLoginAt { get; private set; }

	public UserName LastName { get; private set; } = null!;

	public PasswordHash PasswordHash { get; private set; } = null!;

	public UserPreferences? Preferences { get; private set; }

	public IReadOnlyList<UserRating> Ratings => _ratings.AsReadOnly();

	public DateTimeOffset UpdatedAt { get; set; }

	public IReadOnlyList<UserWatchlist> Watchlist => _watchlist.AsReadOnly();

	public IReadOnlyList<UserWatchProgress> WatchProgress => _watchProgress.AsReadOnly();

	public static ErrorOr<User> Create(
		Email email,
		PasswordHash passwordHash,
		UserName firstName,
		UserName lastName,
		DateOnly dateOfBirth)
	{
		var today = DateOnly.FromDateTime(DateTime.UtcNow);
		var age = today.Year - dateOfBirth.Year;
		if (dateOfBirth.Month > today.Month || (dateOfBirth.Month == today.Month && dateOfBirth.Day > today.Day))
		{
			age--;
		}

		return age switch
		{
			< 13 => DomainErrors.User.DateOfBirth.TooYoung,
			> 120 => DomainErrors.User.DateOfBirth.TooOld,
			_ => new User(UserId.CreateUnique(), email, passwordHash, firstName, lastName, dateOfBirth, true)
		};
	}

	public void Activate()
	{
		IsActive = true;
		UpdatedAt = DateTimeOffset.UtcNow;
	}

	public void AddRating(UserRating rating)
	{
		// Check if already exists
		var existing = _ratings.FirstOrDefault(r =>
			r.ContentId == rating.ContentId && r.ContentType == rating.ContentType);

		if (existing != null)
		{
			// Update existing rating
			_ratings.Remove(existing);
		}

		_ratings.Add(rating);
		UpdatedAt = DateTimeOffset.UtcNow;
	}

	public void AddToWatchlist(UserWatchlist item)
	{
		// Check if already exists
		var existing = _watchlist.FirstOrDefault(w =>
			w.ContentId == item.ContentId && w.ContentType == item.ContentType);

		if (existing == null)
		{
			_watchlist.Add(item);
			UpdatedAt = DateTimeOffset.UtcNow;
		}
	}

	public void Deactivate()
	{
		IsActive = false;
		UpdatedAt = DateTimeOffset.UtcNow;
	}

	public void RemoveFromWatchlist(Guid contentId, ContentType contentType)
	{
		var item = _watchlist.FirstOrDefault(w =>
			w.ContentId == contentId && w.ContentType == contentType);

		if (item != null)
		{
			_watchlist.Remove(item);
			UpdatedAt = DateTimeOffset.UtcNow;
		}
	}

	public void SetPreferences(UserPreferences preferences)
	{
		Preferences = preferences;
		UpdatedAt = DateTimeOffset.UtcNow;
	}

	public void UpdateLastLogin()
	{
		LastLoginAt = DateTimeOffset.UtcNow;
		UpdatedAt = DateTimeOffset.UtcNow;
	}

	public void UpdateWatchProgress(UserWatchProgress progress)
	{
		var existing = _watchProgress.FirstOrDefault(w =>
			w.ContentId == progress.ContentId && w.ContentType == progress.ContentType);

		if (existing != null)
		{
			// Update existing progress
			_watchProgress.Remove(existing);
		}

		_watchProgress.Add(progress);
		UpdatedAt = DateTimeOffset.UtcNow;
	}
}