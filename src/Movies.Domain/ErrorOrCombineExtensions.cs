using ErrorOr;

namespace Movies.Domain;

public static class ErrorOrCombineExtensions
{
	public static List<Error> CombineErrors(params IErrorOr[] errorOrs)
	{
		return errorOrs
			.Where(e => e.IsError)
			.SelectMany(e => e.Errors ?? [])
			.ToList();
	}
}