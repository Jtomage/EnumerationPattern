using EnumerationPattern.StringCases;
using System.Text;

namespace EnumerationPattern;

public abstract class StringCase
{
	/// <summary>
	/// Create Delegate used to transform character
	/// </summary>
	/// <param name="c">Current Character</param>
	/// <param name="isNewWord">if the current character is the start of the new word</param>
	/// <returns></returns>
	protected delegate char ConvertCharacter(char c, bool isNewWord);

	protected ConvertCharacter convertCharacter;

	protected char? seperator;

	protected StringCase(ConvertCharacter convertCharacter, char? seperator = null)
	{
		this.convertCharacter = convertCharacter;
		this.seperator = seperator;
	}

	public virtual string Convert(string text)
	{
		char[] charArray = text.ToCharArray();
		StringBuilder sb = new StringBuilder();

		bool isNewWord = true;
		foreach (char c in charArray)
		{
			if (c == ' ')
			{
				isNewWord = true;
				sb.Append(seperator);
			}
			else
			{
				sb.Append(convertCharacter(c, isNewWord));
				isNewWord = false;
			}
		}

		return sb.ToString();
	}

	#region Static String Cases"

	public static StringCase CamelCase = new CamelCase();

	public static StringCase PascalCase = new PascalCase();

	public static StringCase KebabLowerCase = new KebabLowerCase();

	public static StringCase KebabUpperCase = new KebabUpperCase();

	public static StringCase SnakeLowerCase = new SnakeLowerCase();

	public static StringCase SnakeUpperCase = new SnakeUpperCase();

	#endregion
}
