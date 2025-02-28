using System.Reflection;

namespace Movies.Persistence;

public static class AssemblyReference
{
	public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}