using EnumerationPattern;
using EnumerationPattern.JsonConverters;
using EnumerationPattern.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
	.AddControllers(options =>
	{
		// only needed if applying modelbinder attribute on controller params
		//options.ModelBinderProviders.Insert(0, new EnumerationModelBinderProvider());
	})
	.AddJsonOptions(options =>
	{
		// Add Json Converter for responses
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumerationConverterFactory(StringCase.CamelCase));
	});

//Add Json String Enumeration Converter Factory for minimal api
//builder.Services.ConfigureHttpJsonOptions(options =>
//{
//	options.SerializerOptions.Converters.Add(new JsonStringEnumerationConvertFactory(StringCase.CamelCase));
//});
//builder.Services.Configure<JsonOptions>(options =>
//{
//	options.SerializerOptions.Converters.Add(new JsonStringEnumerationConvertFactory(StringCase.CamelCase));
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	// Add custom schema filter for Enumeration Data
	options.SchemaFilter<SwaggerEnumerationSchemaFilter>(StringCase.CamelCase);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
