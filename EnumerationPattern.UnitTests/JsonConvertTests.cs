using System.Text.Json;

using EnumerationPattern.JsonConverters;

namespace EnumerationPattern.UnitTests;

public class JsonConvertTests
{

	[Fact]
	public void WriteEnumerationToJsonSnakeLowerCase()
	{
		// Arrange
		var testData = new TestData();
		var options = new JsonSerializerOptions()
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			Converters = { new JsonStringEnumerationConverterFactory(StringCase.SnakeLowerCase) }
		};

		// Act
		var result = JsonSerializer.Serialize(testData, options);

		// Assert
		var expected = "{\"auditStatus\":\"not_started\",\"tpsReportStatus\":\"submitted_for_review\",\"wenisStatus\":\"in_review\"}";
		Assert.Equivalent(expected, result);
	}

	[Fact]
	public void WriteEnumerationToJsonCamelCase()
	{
		// Arrange
		var testData = new TestData();
		var options = new JsonSerializerOptions()
		{
			Converters = { new JsonStringEnumerationConverterFactory(StringCase.CamelCase) }
		};

		// Act
		var result = JsonSerializer.Serialize(testData, options);

		// Assert
		var expected = "{\"AuditStatus\":\"notStarted\",\"TPSReportStatus\":\"submittedForReview\",\"WenisStatus\":\"inReview\"}";
		Assert.Equivalent(expected, result);
	}

	[Fact]
	public void ReadEnumerationToJsonPascalCase()
	{
		// Arrange
		// Act
		// Assert
	}

	[Fact]
	public void ReadEnumerationToJsonKebabUpperCase()
	{
		// Arrange
		// Act
		// Assert
	}
}



