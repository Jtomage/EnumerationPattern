namespace EnumerationPattern.StringCases;

internal sealed class CamelCase : StringCase
{
	public CamelCase()
		: base((c, isNewWord) => isNewWord ? char.ToUpper(c) : char.ToLower(c))
	{
	}

	public override string Convert(string text)
	{
		var pascalText = base.Convert(text);

		if (string.IsNullOrWhiteSpace(pascalText))
			return pascalText;

		return char.ToLower(pascalText[0]) + pascalText.Substring(1);

	}
}
