namespace EnumerationPattern.StringCases;

internal sealed class PascalCase : StringCase
{
	public PascalCase()
		: base((c, isNewWord) => isNewWord ? char.ToUpper(c) : char.ToLower(c))
	{
	}
}
