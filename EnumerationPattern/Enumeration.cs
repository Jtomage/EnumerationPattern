using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace EnumerationPattern;

public abstract class Enumeration
{
	public static List<T> GetAll<T>() where T : Enumeration
	{
		var enumList = typeof(T).GetFields(BindingFlags.Public |
												BindingFlags.Static |
												BindingFlags.DeclaredOnly)
							.Select(f => f.GetValue(null))
							.Cast<T>()
							.ToList();

		return enumList;
	}

	public abstract string ToCaseString(StringCase stringCase);

	public static bool TryConvert(object obj, [MaybeNullWhen(false)] out Enumeration? converted)
	{
		Type? objType = obj.GetType();
		converted = null;

		while (objType != null || objType == typeof(object))
		{
			if (objType == typeof(Enumeration))
			{
				converted = (Enumeration)obj;
				return true;
			}

			objType = objType.BaseType;
		}

		return false;
	}

	public static bool TryGenerateGenericEnumertionFromType(Type currentType, [MaybeNullWhen(true)] out Type? genericEnumerationType)
	{
		genericEnumerationType = null;
		Type? runningType = currentType;
		while (runningType != null || runningType == typeof(object))
		{
			if (runningType.IsGenericType && runningType.GetGenericTypeDefinition() == typeof(Enumeration<>))
			{
				genericEnumerationType = runningType;
				return true;
			}

			runningType = runningType.BaseType;
		}

		return false;
	}
}

public abstract class Enumeration<TId>(TId id, string baseText) : Enumeration,
	IComparable where TId : notnull
{
	/// <summary>
	/// Generic type for Id
	/// </summary>
	public TId Id { get; private set; } = id;

	/// <summary>
	/// The base text that will be used for string conversions
	/// </summary>
	public string BaseText { get; private set; } = baseText;

	#region Static Methods

	public static new IEnumerable<TEnum> GetAll<TEnum>() where TEnum : Enumeration<TId>
	{
		var enumList = typeof(TEnum).GetFields(BindingFlags.Public |
														BindingFlags.Static |
														BindingFlags.DeclaredOnly)
									.Select(f => f.GetValue(null))
									.Cast<TEnum>();

		return enumList;
	}

	/// <summary>
	/// Helper mainly for swagger enumeration schema filter
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="stringCase"></param>
	/// <returns></returns>
	public static IEnumerable<string> GetAllAsStringCase<T>(StringCase stringCase) where T : Enumeration<TId>
	{
		return GetAll<T>().Select(f => f.ToCaseString(stringCase));
	}

	#endregion

	#region String methods

	public override string ToString() => BaseText;

	public override string ToCaseString(StringCase stringCase)
	{
		return stringCase.Convert(BaseText);
	}

	#endregion

	#region Parsing

	/// <summary>
	/// Parsing using a function
	/// </summary>
	/// <typeparam name="TEnum"></typeparam>
	/// <param name="allEnums"></param>
	/// <param name="predicate"></param>
	/// <param name="result"></param>
	/// <returns></returns>
	protected static bool TryParse<TEnum>(IEnumerable<TEnum> allEnums, Func<TEnum, bool> predicate, [MaybeNullWhen(false)] out TEnum? result)
		where TEnum : Enumeration<TId>
	{
		result = allEnums.FirstOrDefault(predicate);
		if (result is not null)
			return true;
		else
			return false;
	}

	/// <summary>
	/// Parsing when String Case is known
	/// </summary>
	/// <typeparam name="TEnum"></typeparam>
	/// <param name="stringCase"></param>
	/// <param name="enumString"></param>
	/// <param name="result"></param>
	/// <returns></returns>
	public static bool TryParse<TEnum>(StringCase stringCase, string? enumString, [MaybeNullWhen(false)] out TEnum? result)
		where TEnum : Enumeration<TId>
	{
		// default to null
		result = null;

		if (string.IsNullOrWhiteSpace(enumString))
			return false;

		var all = GetAll<TEnum>();

		return TryParse(all, item => item.ToCaseString(stringCase) == enumString, out result);
	}

	/// <summary>
	/// Will try to parse based on known case strings
	/// </summary>
	public static bool TryParse<TEnum>([NotNullWhen(true)] string? enumString, [MaybeNullWhen(false)] out TEnum? result)
		where TEnum : Enumeration<TId>
	{
		result = null;
		if (string.IsNullOrWhiteSpace(enumString))
			return false;

		var all = GetAll<TEnum>();

		if (TryParse(all, item => item.ToCaseString(StringCase.CamelCase) == enumString, out result))
			return true;
		if (TryParse(all, item => item.ToCaseString(StringCase.PascalCase) == enumString, out result))
			return true;
		if (TryParse(all, item => item.ToCaseString(StringCase.KebabLowerCase) == enumString, out result))
			return true;
		if (TryParse(all, item => item.ToCaseString(StringCase.SnakeLowerCase) == enumString, out result))
			return true;
		if (TryParse(all, item => item.ToCaseString(StringCase.KebabUpperCase) == enumString, out result))
			return true;
		if (TryParse(all, item => item.ToCaseString(StringCase.SnakeUpperCase) == enumString, out result))
			return true;
		if (TryParse(all, item => item.Id.Equals(enumString), out result))
			return true;
		if (TryParse(all, item => item.BaseText.Equals(enumString), out result))
			return true;

		return false;
	}

	public static TEnum FromId<TEnum>(TId id) where TEnum : Enumeration<TId>
	{
		var all = GetAll<TEnum>();
		if (TryParse<TEnum>(all, item => item.Id.Equals(id), out var result))
			if (result is not null) return result;

		throw new InvalidOperationException($"Id, {id}, was not found in {typeof(TEnum)}");
	}

	public static TEnum FromBaseText<TEnum>(string enumText) where TEnum : Enumeration<TId>
	{
		var all = GetAll<TEnum>();
		if (TryParse<TEnum>(all, item => item.BaseText.Equals(enumText), out var result))
			if (result is not null) return result;

		throw new InvalidOperationException($"Id, {enumText}, was not found in {typeof(TEnum)}");
	}

	#endregion

	#region Implementing Comparable

	public override int GetHashCode() => Id.GetHashCode();

	public override bool Equals(object? obj)
	{
		if (obj == null)
			return false;
		if (obj is not Enumeration<TId> otherValue)
			return false;

		var typeMatches = GetType().Equals(obj.GetType());
		var valueMatches = Id.Equals(otherValue.Id);

		return typeMatches && valueMatches;
	}

	public int CompareTo(object? other)
	{
		if (other == null)
			throw new InvalidOperationException("\"other\" object in Enumeration CompareTo is null");
		else if (Id is IComparable comparableId)
			return comparableId.CompareTo(((Enumeration<TId>)other).Id);
		else
			throw new InvalidOperationException("Id must implement Icomparable");
	}

	public static bool operator ==(Enumeration<TId> left, Enumeration<TId> right)
	{
		if (ReferenceEquals(left, right))
			return true;
		if (left is null)
			return false;
		if (right is null)
			return false;
		return left.Equals(right);
	}

	public static bool operator !=(Enumeration<TId> left, Enumeration<TId> right) => !(left == right);

	#endregion
}
