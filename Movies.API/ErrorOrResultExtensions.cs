using ErrorOr;

namespace Movies.API;

public static class ErrorOrResultExtensions
{
	public static async Task<IResult> ToResult<TValue, TResult>(
		this Task<ErrorOr<TValue>> errorOrTask,
		Func<TValue, TResult> onSuccess)
		where TResult : IResult
	{
		var errorOr = await errorOrTask;
		return errorOr.Match(
			value => onSuccess(value),
			errors => errors.ToResult());
	}

	private static IResult ToResult(this List<Error> errors)
	{
		var firstError = errors.FirstOrDefault();
		var statusCode = firstError.Type switch
		{
			ErrorType.Validation => StatusCodes.Status400BadRequest,
			ErrorType.NotFound => StatusCodes.Status404NotFound,
			ErrorType.Conflict => StatusCodes.Status409Conflict,
			ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
			ErrorType.Forbidden => StatusCodes.Status403Forbidden,
			_ => StatusCodes.Status500InternalServerError
		};

		return firstError.Type switch
		{
			ErrorType.Validation => Results.ValidationProblem(errors.ToDictionary(e => e.Code, e => new[]
			{
				e.Description
			})),
			_ => Results.Problem(
				statusCode: statusCode,
				title: firstError.Description,
				type: $"https://httpstatuses.com/{statusCode}",
				extensions: new Dictionary<string, object?>
				{
					{
						"errors", errors.Select(e => new
							{
								e.Code,
								e.Description
							})
							.ToList()
					}
				})
		};
	}
}