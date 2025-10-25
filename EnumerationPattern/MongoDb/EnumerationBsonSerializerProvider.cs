using MongoDB.Bson.Serialization;

namespace EnumerationPattern.MongoDb;

public class EnumerationBsonSerializerProvider : IBsonSerializationProvider
{
	private readonly StringCase _writeStringCase;

	private readonly StringCase? _readStringCase;

	public EnumerationBsonSerializerProvider(StringCase writeStringCase, StringCase? readStringCase = null)
	{
		_writeStringCase = writeStringCase;
		_readStringCase = readStringCase;
	}

	public IBsonSerializer GetSerializer(Type type)
	{
		if (Enumeration.TryGenerateGenericEnumertionFromType(type, out var genericEnumerationType))
		{
			Type enumerationSerialization = typeof(EnumerationBsonSerializer<,>)
				.MakeGenericType([type, .. genericEnumerationType!.GenericTypeArguments]);

			// ignore the null returns 
#pragma warning disable CS8603, CS8600
			return (IBsonSerializer)Activator.CreateInstance(enumerationSerialization, [_writeStringCase, _readStringCase]);
		}

		return null;
#pragma warning restore
	}
}
