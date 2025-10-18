using EnumerationPattern.StringCases;

namespace EnumerationPattern.UnitTests
{
  public class StringCaseTests
  {
    [Fact]
    public void CamelCaseTest()
    {
      // Act
      var text = "tHiS Is a tEsT";

      // Arrange
      var strCase = new CamelCase();
      var result = strCase.Convert(text);

      // Assert
      Assert.Equal("thisIsATest", result);
    }

    [Fact]
    public void PascalCaseTest()
    {
      // Act
      var text = "tHiS Is a tEsT";

      // Arrange
      var strCase = new PascalCase();
      var result = strCase.Convert(text);

      // Assert
      Assert.Equal("ThisIsATest", result);
    }

    [Fact]
    public void KebabLowerCaseTest()
    {
      // Act
      var text = "tHiS Is a tEsT";

      // Arrange
      var strCase = new KebabLowerCase();
      var result = strCase.Convert(text);

      // Assert
      Assert.Equal("this-is-a-test", result);
    }

    [Fact]
    public void KebabUpperCaseTest()
    {
      // Act
      var text = "tHiS Is a tEsT";

      // Arrange
      var strCase = new KebabUpperCase();
      var result = strCase.Convert(text);

      // Assert
      Assert.Equal("THIS-IS-A-TEST", result);
    }

    [Fact]
    public void SnakeLowerCaseTest()
    {
      // Act
      var text = "tHiS Is a tEsT";

      // Arrange
      var strCase = new SnakeLowerCase();
      var result = strCase.Convert(text);

      // Assert
      Assert.Equal("this_is_a_test", result);
    }

    [Fact]
    public void SnakeUpperCaseTest()
    {
      // Act
      var text = "tHiS Is a tEsT";

      // Arrange
      var strCase = new SnakeUpperCase();
      var result = strCase.Convert(text);

      // Assert
      Assert.Equal("THIS_IS_A_TEST", result);
    }

  }
}