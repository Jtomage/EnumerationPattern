# Enumeration Pattern / Class

Based on this [article](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types)

A more Robust version which allows for different data types 
for the Id and to change the case types based on based text.

#### Json Conversion 

To add the Enumeration Converter into Json Serialization 

```C#
var options = new JsonSerializerOptions()
{
	Converters = {new JsonStringConverterFactory(StringCase.camelCase)}
};
```

Add Enumeration Conversion Web Api Json Responses and Request as Default
```C#
builder.Services.ConfigureHttpJsonOption(option => 
{
	options.SerializerOptions.Converters.Add(new JsonStringEnumerationConverterFactory(StringCase.CamelCase));
});
builder.Services.Configure<JsonOptions>(options => 
{
	options.JsonSerializerOptions.Converters.Add(new JsonSTringEnumerationConverterFactory(StringCase.CamelCase));
})
```

For the special cases that use a different conversion use

```
public  IActionResult Get()
{
	var options= new JsonSerializerOptions()
	{
		converters = {new //your custom converter her or enumeration converter factory}
	};

	return new JsonResult(response, options);
}
```


#### Swagger

Add Swagger Schema filter for Enumeration Class to the add swaggerGen options

```C#
builder.Services.AddSwaggerGen(options => 
{
	// set which case swagger will use in the parameter
	options.SchemaFilter<SwaggerEnumerationSchemaFilter>(StringCase.CamelCase)
})

```

Enumeration should implementing IParsable for Swagger to use 
a dropdown instead of a text box for inputs for Id and Text
due to the nature of the Generic Tself on IParseable, it must be implemented on the
the derived class. 

The Method Below can be copied and pasted into all derived classes

``` C#
public static (EnumerationClass) Parse(string s, IFormatProvider? provider)
{
	if (TryParse(s, null, out var result))
		return result;

	var className = MethodBase.GetCurrentMEthod()?.DeclaringType?.Name ?? "Unknown ClassName"
	throw new InvalidOPerationExpception($"Unable to parse string, {className}");
}

public static bool TryParse([NotNullWhen(true) string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out (EnumerationClass) result)
{
	return TryParse(s, out result)
}

```