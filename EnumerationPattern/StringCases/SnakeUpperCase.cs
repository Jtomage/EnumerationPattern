namespace EnumerationPattern.StringCases;

internal sealed class SnakeUpperCase : StringCase
{
	public SnakeUpperCase()
		: base((c, isNewWord) => char.ToUpper(c), '_')
	{
	}
}
