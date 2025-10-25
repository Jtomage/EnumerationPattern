using EnumerationPattern.MongoDb;
using Mongo2Go;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace EnumerationPattern.DataTests.MongoDb;

public class InMemoryMongoDb : IDisposable
{
  private readonly MongoDbRunner _runner;

  private readonly MongoClient _client;

  public IMongoDatabase Database { get; private set; }

  public InMemoryMongoDb(string databaseName)
  {
    _runner = MongoDbRunner.Start();
    _client = new MongoClient(_runner.ConnectionString);
    Database = _client.GetDatabase(databaseName);

    // register guid serializer
    BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

    // register custom serializer provider
    BsonSerializer.RegisterSerializationProvider(new EnumerationBsonSerializerProvider(StringCase.KebabLowerCase));
  }

  public void Dispose()
  {
    _client.Dispose();
    _runner.Dispose();
  }
}
