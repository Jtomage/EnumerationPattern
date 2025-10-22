using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace EnumerationPattern.Swagger;

/// <summary>
/// Enumeration using IParsable&lt;TSelf&gt; which allows swagger 
/// to use dropdowns for the string conversion instead of 
/// </summary>
/// <typeparam name="TId"></typeparam>
/// <typeparam name="TSelf"></typeparam>
public abstract class EnumerationParsable<TId, TSelf> : Enumeration<TId>,
	IParsable<TSelf> where TSelf :
		EnumerationParsable<TId, TSelf> where TId : notnull
{

	public EnumerationParsable(TId id, string baseText)
		: base(id, baseText)
	{
	}

	public static TSelf Parse(string s, IFormatProvider? provider)
	{
		if (TryParse(s, null, out var result))
			return result;

		var className = MethodBase.GetCurrentMethod()?.DeclaringType?.Name ?? "Unknown ClassName";
		throw new InvalidOperationException($"Unable to parse string, {className}");

	}

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
	{
		return TryParse(s, out result);
	}
}
