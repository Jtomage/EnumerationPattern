namespace EnumerationPattern.UnitTests
{
	public class EnumerationTests
	{

		#region Conversion Tests

		[Fact]
		public void TryConvertIfDerviedEnumerationIsEnumerationTest()
		{
			// Arrange
			var workFlowEnum = WorkflowStatus.Touched;
			var testDate = new TestData();

			// Act
			var result = Enumeration.TryConvert(workFlowEnum, out var converted);
			var result2 = Enumeration.TryConvert(testDate, out var converted2);

			// Assert
			Assert.True(result);
			Assert.NotNull(converted);
			Assert.IsAssignableFrom<Enumeration>(converted);

			Assert.False(result2);
			Assert.Null(converted2);
		}

		[Fact]
		public void TryGenerateGenericEnumerationType()
		{

			// Act
			var result = Enumeration.TryGenerateGenericEnumertionFromType(typeof(WorkflowStatus), out var genericEnumerationType);
			var result2 = Enumeration.TryGenerateGenericEnumertionFromType(typeof(TestData), out var genericEnumerationType2);

			// Assert
			Assert.True(result);
			Assert.NotNull(genericEnumerationType);
			Assert.True(genericEnumerationType == typeof(Enumeration<Guid>));   //since workflowstatus used guid for generic param

			Assert.False(result2);
			Assert.Null(genericEnumerationType2);
		}

		#endregion

		[Fact]
		public void FromIdTest()
		{
			// Act
			var result = WorkflowStatus.FromId<WorkflowStatus>(WorkflowStatus.InProgress.Id);

			// Assert
			Assert.Equal(WorkflowStatus.InProgress, result);
		}

		[Fact]
		public void FromBaseTextTest()
		{
			// Act
			var result = WorkflowStatus.FromBaseText<WorkflowStatus>(WorkflowStatus.Touched.BaseText);

			// Assert
			Assert.Equal(WorkflowStatus.Touched, result);
		}

	}
}
