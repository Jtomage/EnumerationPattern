namespace EnumerationPattern.StringCases;

internal sealed class KebabUpperCase : StringCase
{
	public KebabUpperCase()
		: base((c, isNewWord) => char.ToUpper(c), '-')
	{
	}
}
