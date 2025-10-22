using System.Text.Json;

using EnumerationPattern.JsonConverters;
using EnumerationPattern.WebDemo.Models;

using Microsoft.AspNetCore.Mvc;

namespace EnumerationPattern.WebDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnumerationController : ControllerBase
{

	/// <summary>
	/// Will return the same status that was passed in 
	/// </summary>
	/// <param name="status">The workflow status using IParsable</param>
	/// <param name="paymentType">The payment type</param>
	/// <param name="relationshipType">
	///		Will work with string case but will show 2 values 
	///		as swagger flattens the properties
	/// </param>
	/// <returns>Simple Model</returns>
	[HttpGet]
	public ActionResult<AdvanceModel> Get([FromQuery] WorkflowStatus? status, [FromQuery] PaymentType? paymentType, [FromQuery] SqlRelationshipType? relationshipType)
	{
		var response = new AdvanceModel()
		{
			WorkflowStatus = status,
			PaymentType = paymentType,
			RelationshipType = relationshipType
		};
		return Ok(response);
	}

	/// <summary>
	/// Show how swagger will use Drop Downs 
	/// if implementing IParsable on Enumeration
	/// vs using model binding
	/// </summary>
	/// <param name="model">Show Advance model using Polymetric IParsable, IParseable per Class and model binding</param>
	/// <returns>Snake Upper Case of Advance Model</returns>
	[HttpGet("Advance")]
	public ActionResult<string> Get([FromQuery] AdvanceModel? model)
	{
		var options = new JsonSerializerOptions()
		{
			Converters = { new JsonStringEnumerationConvertFactory(StringCase.SnakeUpperCase) }
		};

		return new JsonResult(model, options);
	}


	/// <summary>
	/// Will return the same Simple Model back in Kebab Lower Case showing how 
	/// to customize the case return back
	/// </summary>
	/// <param name="model"></param>
	/// <returns>Simple Model with workflow in kebab lower case</returns>
	[HttpPost]
	public ActionResult<string> Post([FromBody] SimpleModel model)
	{
		var options = new JsonSerializerOptions()
		{
			Converters = { new JsonStringEnumerationConvertFactory(StringCase.KebabLowerCase) },
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};

		return new JsonResult(model, options);
	}

	/// <summary>
	/// Returning an advance modal in Snake Lower Case
	/// 
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	[HttpPost("Advance")]
	public ActionResult<string> AdvancePost([FromBody] AdvanceModel model)
	{
		var options = new JsonSerializerOptions()
		{
			Converters = { new JsonStringEnumerationConvertFactory(StringCase.SnakeLowerCase) },
		};

		return new JsonResult(model, options);
	}
}
