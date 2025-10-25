using MongoDB.Bson;
using MongoDB.Driver;

namespace EnumerationPattern.DataTests.MongoDb;

public class MongoDbEnumerationTests : IDisposable
{
  private readonly InMemoryMongoDb _mongoDb;

  public MongoDbEnumerationTests()
  {
    _mongoDb = new InMemoryMongoDb("testDatabase");
  }

  [Fact]
  public void InsertTest()
  {
    // Arrange
    var collection = _mongoDb.Database.GetCollection<SimpleModel>("SampleModel");
    var data = new SimpleModel()
    {
      Id = 43904,
      Status = WorkflowStatus.SubmittedForReview
    };

    // Act
    collection.InsertOne(data);

    // Assert
    var result = collection.Find(f => f.Status == WorkflowStatus.SubmittedForReview).First();
    Assert.Equivalent(data, result);

    // raw data Assert
    var rawData = _mongoDb.Database.GetCollection<BsonDocument>("SampleModel").Find(new BsonDocument()).First();
    Assert.Equal("submitted-for-review", rawData.GetValue("Status").AsString);
  }

  public void Dispose()
  {
    _mongoDb.Dispose();
  }
}
