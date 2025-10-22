using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EnumerationPattern.ModelBinder;

public class EnumerationModelBinder<TEnum, TId> : IModelBinder
	where TEnum : Enumeration<TId> where TId : notnull
{
	public Task BindModelAsync(ModelBindingContext bindingContext)
	{
		// get the value 
		var rawValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
		if (!string.IsNullOrWhiteSpace(rawValue))
		{
			if (Enumeration<TId>.TryParse<TEnum>(rawValue, out var enumResult))
			{
				bindingContext.Result = ModelBindingResult.Success(enumResult);
			}

		}
		return Task.CompletedTask;
	}
}
