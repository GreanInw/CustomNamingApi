using Microsoft.AspNetCore.Mvc.ModelBinding;
using POC_NamingApi.Helpers;

namespace POC_NamingApi.ModelBinders
{
    public class SnakeCaseModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));
            if (!SnakeCaseNamingHelper.IsSnakeCaseObject(bindingContext.ModelType))
            {
                return Task.CompletedTask;
            }

            var model = Activator.CreateInstance(bindingContext.ModelType);
            foreach (var property in bindingContext.ModelType.GetProperties())
            {
                var value = bindingContext.ValueProvider as CompositeValueProvider;
                
                //Get value
                var result = bindingContext.ValueProvider.GetValue(property.Name.ToSnakeCase());
                
                if (result == ValueProviderResult.None)
                {
                    continue;
                }

                //Convert string to original type.
                var newValue = TypeConverterHelper.ConvertTo(result.FirstValue, property.PropertyType);
                //Set value into model.
                property.SetValue(model, newValue);
            }

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}
