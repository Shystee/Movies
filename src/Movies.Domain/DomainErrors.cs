using ErrorOr;

namespace Movies.Domain;

internal static class DomainErrors
{
	public static class Episode
	{
		public static Error InvalidEpisodeId => Error.Validation("Episode.InvalidId", "Episode ID is invalid");
	}

	public static class Genre
	{
		public static Error InvalidGenreId => Error.Validation("Genre.InvalidId", "Genre ID is invalid");

		public static class Description
		{
			public static Error Empty => Error.Validation("Genre.Description.Empty", "Genre description can't be empty");

			public static Error TooLong => Error.Validation("Genre.Description.TooLong", "Genre description is too long");
		}

		public static class Name
		{
			public static Error Empty => Error.Validation("Genre.Name.Empty", "Genre name can't be empty");

			public static Error TooLong => Error.Validation("Genre.Name.TooLong", "Genre name is too long");
		}
	}

	public static class Movie
	{
		public static Error InvalidMovieId => Error.Validation("Movie.InvalidId", "Movie ID is invalid");

		public static class AgeRestriction
		{
			public static Error Invalid =>
				Error.Validation("Movie.InvalidAgeRestriction", "Movie age restriction is not a recognized rating");
		}

		public static class Description
		{
			public static Error Empty => Error.Validation("Movie.Description.Empty", "Movie description can't be empty");

			public static Error TooLong => Error.Validation("Movie.Description.TooLong", "Movie description is too long");
		}

		public static class Duration
		{
			public static Error Invalid => Error.Validation("Movie.InvalidDuration", "Movie duration must be between 1 and 600 minutes");
		}

		public static class Rating
		{
			public static Error Invalid => Error.Validation("Movie.UserRating.Invalid", "Movie rating should be between 1 and 5");

			public static Error InvalidAverage =>
				Error.Validation("Movie.AverageRating.Invalid", "Movie average rating should be between 0 and 10");

			public static Error InvalidCount => Error.Validation("Movie.RatingCount.Invalid", "Movie rating count cannot be negative");
		}

		public static class ReleaseDate
		{
			public static Error Invalid => Error.Validation("Movie.InvalidReleaseDate", "Movie release date is invalid");
		}

		public static class ReleaseYear
		{
			public static Error Invalid => Error.Validation("Movie.InvalidReleaseYear", "Movie release year is invalid");
		}

		public static class Tagline
		{
			public static Error TooLong => Error.Validation("Movie.Tagline.TooLong", "Movie tagline is too long");
		}

		public static class Title
		{
			public static Error Empty => Error.Validation("Movie.Title.Empty", "Movie title can't be empty");

			public static Error TooLong => Error.Validation("Movie.Title.TooLong", "Movie title is too long");
		}
	}

	public static class PasswordHash
	{
		public static Error Empty => Error.Validation("User.Password.Empty", "Password can't be empty");
	}

	public static class Person
	{
		public static Error InvalidPersonId => Error.Validation("Person.InvalidId", "Person ID is invalid");

		public static class Biography
		{
			public static Error Empty => Error.Validation("Person.Biography.Empty", "Person biography can't be empty");

			public static Error TooLong => Error.Validation("Person.Biography.TooLong", "Person biography is too long");
		}

		public static class BirthDate
		{
			public static Error Invalid => Error.Validation("Person.BirthDate.Invalid", "Person birth date is invalid");
		}

		public static class Name
		{
			public static Error Empty => Error.Validation("Person.Name.Empty", "Person name can't be empty");

			public static Error TooLong => Error.Validation("Person.Name.TooLong", "Person name is too long");
		}
	}

	public static class Season
	{
		public static Error InvalidSeasonId => Error.Validation("Season.InvalidId", "Season ID is invalid");
	}

	public static class TvShow
	{
		public static Error InvalidTvShowId => Error.Validation("TvShow.InvalidId", "TvShow ID is invalid");
	}

	public static class Url
	{
		public static Error Invalid => Error.Validation("Common.Url.Invalid", "URL format is invalid");

		public static Error TooLong => Error.Validation("Common.Url.TooLong", "URL is too long");
	}

	public static class User
	{
		public static Error InvalidUserId => Error.Validation("User.InvalidId", "User ID is invalid");

		public static class DateOfBirth
		{
			public static Error Invalid => Error.Validation("User.DateOfBirth.Invalid", "Date of birth is invalid");

			public static Error TooYoung => Error.Validation("User.DateOfBirth.TooYoung", "User must be at least 13 years old");

			public static Error TooOld => Error.Validation("User.DateOfBirth.TooOld", "User date of birth is too far in the past");
		}

		public static class Email
		{
			public static Error Empty => Error.Validation("User.Email.Empty", "Email can't be empty");

			public static Error Invalid => Error.Validation("User.Email.Invalid", "Email format is invalid");

			public static Error TooLong => Error.Validation("User.Email.TooLong", "Email is too long");
		}

		public static class Name
		{
			public static Error Empty => Error.Validation("User.Name.Empty", "Name can't be empty");

			public static Error TooLong => Error.Validation("User.Name.TooLong", "Name is too long");
		}

		public static class Password
		{
			public static Error MissingDigit =>
				Error.Validation("User.Password.MissingDigit", "Password must contain at least one digit");

			public static Error MissingLowercase =>
				Error.Validation("User.Password.MissingLowercase", "Password must contain at least one lowercase letter");

			public static Error MissingSpecialChar =>
				Error.Validation("User.Password.MissingSpecialChar", "Password must contain at least one special character");

			public static Error MissingUppercase =>
				Error.Validation("User.Password.MissingUppercase", "Password must contain at least one uppercase letter");

			public static Error TooLong => Error.Validation("User.Password.TooLong", "Password is too long");

			public static Error TooShort => Error.Validation("User.Password.TooShort", "Password is too short");
		}

		public static class Preferences
		{
			public static Error InvalidLanguage =>
				Error.Validation("User.Preferences.InvalidLanguage", "Selected language is not supported");

			public static Error InvalidQuality =>
				Error.Validation("User.Preferences.InvalidQuality", "Selected streaming quality is not supported");
		}

		public static class Rating
		{
			public static Error AlreadyExists => Error.Conflict("User.Rating.AlreadyExists", "User has already rated this content");

			public static Error InvalidReview => Error.Validation("User.Rating.InvalidReview", "Review text is too long");

			public static Error InvalidValue => Error.Validation("User.Rating.InvalidValue", "Rating value must be between 1 and 5");

			public static Error NotFound => Error.NotFound("User.Rating.NotFound", "Rating not found");
		}

		public static class Watchlist
		{
			public static Error AlreadyExists => Error.Conflict("User.Watchlist.AlreadyExists", "Content is already in the watchlist");

			public static Error NotFound => Error.NotFound("User.Watchlist.NotFound", "Content not found in watchlist");
		}

		public static class WatchProgress
		{
			public static Error InvalidPercentage =>
				Error.Validation("User.WatchProgress.InvalidPercentage", "Watch percentage must be between 0 and 100");

			public static Error InvalidPosition =>
				Error.Validation("User.WatchProgress.InvalidPosition", "Watch position cannot be negative");
		}
	}

	public static class VideoAsset
	{
		public static Error InvalidVideoAssetId => Error.Validation("VideoAsset.InvalidId", "VideoAsset ID is invalid");

		public static class Duration
		{
			public static Error Invalid => Error.Validation("VideoAsset.Duration.Invalid", "Duration must be positive");
		}

		public static class FileFormat
		{
			public static Error Empty => Error.Validation("VideoAsset.FileFormat.Empty", "File format can't be empty");

			public static Error Invalid => Error.Validation("VideoAsset.FileFormat.Invalid", "File format is not supported");
		}

		public static class FilePath
		{
			public static Error Empty => Error.Validation("VideoAsset.FilePath.Empty", "File path can't be empty");

			public static Error Invalid => Error.Validation("VideoAsset.FilePath.Invalid", "File path format is invalid");

			public static Error TooLong => Error.Validation("VideoAsset.FilePath.TooLong", "File path is too long");
		}

		public static class FileSize
		{
			public static Error Invalid => Error.Validation("VideoAsset.FileSize.Invalid", "File size must be positive");
		}

		public static class StorageBucket
		{
			public static Error Empty => Error.Validation("VideoAsset.StorageBucket.Empty", "Storage bucket can't be empty");

			public static Error TooLong => Error.Validation("VideoAsset.StorageBucket.TooLong", "Storage bucket name is too long");
		}

		public static class Title
		{
			public static Error Empty => Error.Validation("VideoAsset.Title.Empty", "Video asset title can't be empty");

			public static Error TooLong => Error.Validation("VideoAsset.Title.TooLong", "Video asset title is too long");
		}
	}

	public static class VideoStream
	{
		public static Error InvalidVideoStreamId => Error.Validation("VideoStream.InvalidId", "VideoStream ID is invalid");

		public static class Format
		{
			public static Error Invalid => Error.Validation("VideoStream.Format.Invalid", "Stream format is not supported");
		}

		public static class ManifestUrl
		{
			public static Error Empty => Error.Validation("VideoStream.ManifestUrl.Empty", "Manifest URL can't be empty");

			public static Error Invalid => Error.Validation("VideoStream.ManifestUrl.Invalid", "Manifest URL format is invalid");
		}

		public static class Quality
		{
			public static Error Invalid => Error.Validation("VideoStream.Quality.Invalid", "Stream quality is not valid");
		}
	}
}