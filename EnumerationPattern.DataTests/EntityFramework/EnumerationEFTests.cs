using Microsoft.EntityFrameworkCore;

namespace EnumerationPattern.DataTests.EntityFramework
{
  public class EnumerationEFTests : IDisposable
  {

    private readonly EnumerationContext _context;

    public EnumerationEFTests()
    {
      var options = new DbContextOptionsBuilder()
        .UseInMemoryDatabase("test database")
        .Options;
      _context = new EnumerationContext(options);
      _context.Database.EnsureCreated();

    }

    [Fact]
    public async Task InsertTest()
    {
      // Arrange
      var data = new SimpleModel()
      {
        Id = 1,
        Status = WorkflowStatus.SubmittedForReview
      };

      // Act
      await _context.SampleModels.AddAsync(data);
      await _context.SaveChangesAsync();

      // Assert
      // there should only be 1 value
      var result = _context.SampleModels.First();
      Assert.Equivalent(data, result);
      // using the where clause
      var result2 = _context.SampleModels.Where(w => w.Status == data.Status).First();
      Assert.Equivalent(data, result2);
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
