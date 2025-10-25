using System.Text.Json;
using System.Text.Json.Serialization;

namespace EnumerationPattern.JsonConverters;

public class JsonStringEnumerationConverterFactory : JsonConverterFactory
{
	private readonly StringCase _writeStringCase;

	private readonly StringCase? _readStringCase;

	public JsonStringEnumerationConverterFactory(StringCase writeStringCase, StringCase? readStringCase = null)
	{
		_writeStringCase = writeStringCase;
		_readStringCase = readStringCase;
	}

	public override bool CanConvert(Type typeToConvert)
	{
		return Enumeration.TryGenerateGenericEnumertionFromType(typeToConvert, out var _);
	}

	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		if (Enumeration.TryGenerateGenericEnumertionFromType(typeToConvert, out var genericEnumerationType))
		{
			// enumeration type should not be null to due to try logic
			// create JsonStringEnumerationConverter using return generic type
			Type converterType = typeof(JsonStringEnumerationConverter<,>)
				.MakeGenericType([typeToConvert, .. genericEnumerationType!.GenericTypeArguments]);

			return (JsonConverter?)Activator.CreateInstance(converterType, [_writeStringCase, _readStringCase]);
		}

		throw new InvalidOperationException("Unable to create JsonConverter as type is NOT of Enumeration<T>");
	}
}
