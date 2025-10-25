using Microsoft.EntityFrameworkCore;

namespace EnumerationPattern.DataTests.EntityFramework;

public class EnumerationContext : DbContext
{

  public EnumerationContext(DbContextOptions options)
    : base(options)
  { }

  public DbSet<SimpleModel> SampleModels { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<SimpleModel>(entity =>
    {
      entity.HasKey(x => x.Id);

      // add conversion to use Workflow status when stored in database
      entity.Property(p => p.Status)
        .HasConversion
        (
          c => c.Id,
          c => WorkflowStatus.FromId<WorkflowStatus>(c)
        );
    });
  }

}
