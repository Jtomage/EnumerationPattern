using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EnumerationPattern.ModelBinder;

public class EnumerationModelBinderProvider : IModelBinderProvider
{
	public IModelBinder? GetBinder(ModelBinderProviderContext context)
	{

		if (Enumeration.TryGenerateGenericEnumertionFromType(context.Metadata.ModelType, out var genericEnumerationType))
		{
			Type modelBinderType = typeof(EnumerationModelBinder<,>)
				.MakeGenericType([context.Metadata.ModelType, .. genericEnumerationType!.GenericTypeArguments]);

			return (IModelBinder?)Activator.CreateInstance(modelBinderType);
		}

		return null;

		//if (context.Metadata.ModelType != typeof(Device))
		//{
		//	return null;
		//}

		//var subclasses = new[] { typeof(Laptop), typeof(SmartPhone), };

		//var binders = new Dictionary<Type, (ModelMetadata, IModelBinder)>();
		//foreach (var type in subclasses)
		//{
		//	var modelMetadata = context.MetadataProvider.GetMetadataForType(type);
		//	binders[type] = (modelMetadata, context.CreateBinder(modelMetadata));
		//}

		//return new DeviceModelBinder(binders);
	}
}
