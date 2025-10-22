# Enumeration Pattern / Class

Based on this [article](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types)

A more Robust version which allows for different data types 
for the Id and to change the case types based on based text.

## Json Conversion 

To add the Enumeration Converter into Json Serialization 

```C#
var options = new JsonSerializerOptions()
{
	Converters = {new JsonStringConverterFactory(StringCase.camelCase)}
};
```

Add Enumeration Conversion Web Api Json Responses and Request as Default
```C#
builder.Services.ConfigureHttpJsonOptions(options =>
{
	options.SerializerOptions.Converters.Add(new JsonStringEnumerationConvertFactory(StringCase.CamelCase));
});
builder.Services.Configure<JsonOptions>(options =>
{
	options.SerializerOptions.Converters.Add(new JsonStringEnumerationConvertFactory(StringCase.CamelCase));
});
```

For the special cases that use a different conversion use

```
public  IActionResult Get()
{
	var options= new JsonSerializerOptions()
	{
		converters = {new //your custom converter here or enumeration converter factory}
	};

	return new JsonResult(response, options);
}
```


## Swagger

Add Swagger Schema filter for Enumeration Class to the add swaggerGen options

```C#
builder.Services.AddSwaggerGen(options => 
{
	// set which case swagger will use in the parameter
	options.SchemaFilter<SwaggerEnumerationSchemaFilter>(StringCase.CamelCase)
})

```

However Swagger still needs IParsable&lt;TSelf&gt; or ModelBinder to be implemented
to display the dropdowns. The Controller will still work if called directly

IParsable&lt;TSelf&gt; can be implemented on each class

The Method Below can be copied and pasted into all derived classes

``` C#
public static (EnumerationClass) Parse(string s, IFormatProvider? provider)
{
	if (TryParse(s, null, out var result))
			return result;

		var className = MethodBase.GetCurrentMethod()?.DeclaringType?.Name ?? "Unknown ClassName";
		throw new InvalidOperationException($"Unable to parse string, {className}");
}

public static bool TryParse([NotNullWhen(true) string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out (EnumerationClass) result)
{
	return TryParse(s, out result);
}

```

#### OR

Be done Morphic that can been seen on the EnumerationParsable class in the swagger
folder.

Also included is an exmple using ModelBinder. 


## Data Layer

TO DO show examples of how use Enumeration Pattern / class on the data layer

### Entity Framework

### MongoDb