namespace EnumerationPattern.WebDemo.Models;

/// <summary>
/// Model Binding Attribute below can be added to the whole class 
/// which will add dropdowns to swagger
/// </summary>
/// <param name="id"></param>
/// <param name="baseText"></param>
// [ModelBinder(BinderType = typeof(EnumerationModelBinder<SqlRelationshipType, string>))]
public class SqlRelationshipType(string id, string baseText)
	: Enumeration<string>(id, baseText)
{
	public static SqlRelationshipType One2One = new SqlRelationshipType("1 to 1", "One to One");
	public static SqlRelationshipType One2Many = new SqlRelationshipType("1 to N", "One to Many");
	public static SqlRelationshipType Many2Many = new SqlRelationshipType("N to N", "Many to Many");
}
