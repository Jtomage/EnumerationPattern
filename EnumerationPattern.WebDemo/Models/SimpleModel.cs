namespace EnumerationPattern.WebDemo.Models;

public class SimpleModel
{
	public int Id { get; set; }

	public WorkflowStatus Status { get; set; } = WorkflowStatus.NotStarted;
}
