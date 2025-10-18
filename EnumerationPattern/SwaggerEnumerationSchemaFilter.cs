using System.Reflection;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace EnumerationPattern;

public class SwaggerEnumerationSchemaFilter : ISchemaFilter
{
	public readonly StringCase _enumerationPresetCase;

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="enumerationPresetCase">Set the preset that swagger will use</param>
	public SwaggerEnumerationSchemaFilter(StringCase enumerationPresetCase)
	{
		_enumerationPresetCase = enumerationPresetCase;
	}

	public void Apply(OpenApiSchema schema, SchemaFilterContext context)
	{
		if (context.Type != null && Enumeration.TryGenerateGenericEnumertionFromType(context.Type, out var genericEnumerationType))
		{
			//var getAllAsStringCase = typeof(Enumeration<>)
			//	.MakeGenericType(genericEnumerationType!.GenericTypeArguments)
			//	.GetMethod("GetAllAsStringCase", BindingFlags.Static | BindingFlags.Public);

			var getAllAsStringCase = genericEnumerationType!
				.GetMethod("GetAllAsStringCase", BindingFlags.Static | BindingFlags.Public);

			if (getAllAsStringCase != null)
			{
				// create and invoke the method with generic
				var generic = getAllAsStringCase.MakeGenericMethod(context.Type);
				var result = (IEnumerable<string>?)generic.Invoke(null, new[] { _enumerationPresetCase });

				if (result != null)
				{
					schema.Type = "string";
					schema.Enum = result.Select(s => new OpenApiString(s)).Cast<IOpenApiAny>().ToList();
				}
			}
		}
	}
}
