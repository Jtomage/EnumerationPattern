using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace EnumerationPattern.MongoDb
{
	public class EnumerationBsonSerializer<TEnum, TId>
		: SerializerBase<TEnum> where TEnum : Enumeration<TId> where TId : notnull
	{

		private readonly StringCase _writeStringCase;

		private readonly StringCase? _readStringCase;

		public EnumerationBsonSerializer(StringCase writeStringCase, StringCase? readStringCase = null)
		{
			_writeStringCase = writeStringCase;
			_readStringCase = readStringCase;
		}

		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TEnum value)
		{
			context.Writer.WriteString(value.ToCaseString(_writeStringCase));
		}

		public override TEnum Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			var reader = context.Reader;
			if (reader.GetCurrentBsonType() == BsonType.String)
			{
				if (_readStringCase != null)
				{
					if (Enumeration<TId>.TryParse<TEnum>(_readStringCase, reader.ReadString(), out var value))
						return value;
				}
				else
				{
					if (Enumeration<TId>.TryParse<TEnum>(reader.ReadString(), out var value))
						return value;

				}
			}

			throw new BsonSerializationException("Unable to Deserialize Type");
		}

	}
}
