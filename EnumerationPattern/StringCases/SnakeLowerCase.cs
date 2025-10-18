namespace EnumerationPattern.StringCases;

internal sealed class SnakeLowerCase : StringCase
{
	public SnakeLowerCase()
		: base((c, isNewWord) => char.ToLower(c), '_')
	{
	}
}
