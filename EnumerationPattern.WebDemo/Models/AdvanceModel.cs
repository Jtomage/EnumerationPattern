using EnumerationPattern.ModelBinder;

using Microsoft.AspNetCore.Mvc;

namespace EnumerationPattern.WebDemo.Models
{
	public class AdvanceModel
	{
		public WorkflowStatus WorkflowStatus { get; set; } = WorkflowStatus.NotStarted;

		public PaymentType? PaymentType { get; set; }

		[ModelBinder(BinderType = typeof(EnumerationModelBinder<SqlRelationshipType, string>))]
		public SqlRelationshipType? RelationshipType { get; set; }
	}
}
