using Asp.Versioning;

namespace Movies.API.Endpoints;

internal static class IdentityConfiguration
{
	public const string IdentityPrefixUri = "api/v{version:apiVersion}";

	public static ApiVersion Default { get; } = new(1, 0);
}