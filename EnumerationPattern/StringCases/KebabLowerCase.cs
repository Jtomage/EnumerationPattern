namespace EnumerationPattern.StringCases
{
	internal sealed class KebabLowerCase : StringCase
	{
		public KebabLowerCase()
			: base((c, isNewWord) => char.ToLower(c), '-')
		{
		}
	}
}
