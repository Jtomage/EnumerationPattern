namespace EnumerationPattern.DataTests;

/// <summary>
/// Enumeration Class
/// </summary>
public class WorkflowStatus(Guid id, string baseText)
  : Enumeration<Guid>(id, baseText)
{

  public static WorkflowStatus NotStarted = new WorkflowStatus(new Guid("93115DF2-F214-4B23-BC1E-7D8F857D8F95"), "Not Started");
  public static WorkflowStatus Touched = new WorkflowStatus(new Guid("C02D333B-B6E9-4CF7-9EED-0A5FD6A776F6"), "Touched");
  public static WorkflowStatus InProgress = new WorkflowStatus(new Guid("D392EB06-D270-4982-B8F8-BA4B10518FE4"), "In Progress");
  public static WorkflowStatus SubmittedForReview = new WorkflowStatus(new Guid("465CCE28-F9B8-49E0-BA2D-64DEC9C5999E"), "Submitted For Review");
  public static WorkflowStatus InReview = new WorkflowStatus(new Guid("FD492281-1FD7-4EE5-B837-8C358DA95514"), "In Review");
  public static WorkflowStatus Completed = new WorkflowStatus(new Guid("C0428531-8CD9-4950-90F1-5A81E6460C1A"), "Completed");

}