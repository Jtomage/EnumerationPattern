using System.Text.Json;
using System.Text.Json.Serialization;

namespace EnumerationPattern.JsonConverters
{
	public class JsonStringEnumerationConverter<TEnum, TId>
		: JsonConverter<TEnum> where TEnum : Enumeration<TId> where TId : notnull
	{

		private readonly StringCase _writeStringCase;

		private readonly StringCase? _readStringCase;

		public JsonStringEnumerationConverter(StringCase writeStringCase, StringCase? readStringCase = null)
		{
			_writeStringCase = writeStringCase;
			_readStringCase = readStringCase;
		}

		/// <summary>
		/// Check if possible to convert
		/// </summary>
		/// <param name="typeToConvert"></param>
		/// <returns></returns>
		public override bool CanConvert(Type typeToConvert)
		{
			return Enumeration.TryGenerateGenericEnumertionFromType(typeToConvert, out var genericEnumType);
		}

		public override TEnum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String)
			{
				var stringVal = reader.GetString();
				if (_readStringCase != null)
				{
					if (Enumeration<TId>.TryParse<TEnum>(_readStringCase, stringVal, out var value))
						return value;
				}
				else
				{
					// use brute parsing
					if (Enumeration<TId>.TryParse<TEnum>(stringVal, out var value))
						return value;
				}
				throw new JsonException($"JsonStringEnumerationConverter failed to parse json, {stringVal}");

			}

			throw new JsonException($"JsonStringEnumerationConverter failed to parse as token was NOT string");
		}

		public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToCaseString(_writeStringCase));
		}
	}
}
