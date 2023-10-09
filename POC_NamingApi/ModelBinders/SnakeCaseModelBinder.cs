using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Options;
using POC_NamingApi.Attributes;
using POC_NamingApi.Controllers;
using POC_NamingApi.Helpers;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace POC_NamingApi.ModelBinders
{
    public class SnakeCaseModelBinder : IModelBinder
    {
        internal Type[] FormFileTypes = new[] { typeof(IFormFile), typeof(IFormFileCollection) };
        private readonly IModelBinder _modelBinder;

        //public SnakeCaseModelBinder() : this(null) { }

        public SnakeCaseModelBinder(IModelBinder modelBinder)
        {
            _modelBinder = modelBinder;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            //if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));
            //if (!SnakeCaseNamingHelper.IsSnakeCaseObject(bindingContext.ModelType))
            //{
            //    return;
            //}

            //var model = Activator.CreateInstance(bindingContext.ModelType);
            //foreach (var property in bindingContext.ModelType.GetProperties())
            //{
            //    //Validate ignore property
            //    if (property.GetCustomAttribute<IgnoreSnakeCaseAttribute>() is not null)
            //    {
            //        continue;
            //    }

            //    object newValue;
            //    if (IsFormFileType(property))//Get file.
            //    {
            //        newValue = GetFiles(bindingContext.HttpContext, property.PropertyType, property.Name);
            //    }
            //    else //Get value
            //    {
            //        var resultValue = GetValue(bindingContext.ValueProvider
            //            , property.PropertyType, property.Name);
            //        if (!resultValue.IsNotFound)
            //        {
            //            continue;
            //        }
            //        newValue = resultValue.Value;
            //    }

            //    //Set value into model.
            //    property.SetValue(model, newValue);
            //}
            //bindingContext.Result = ModelBindingResult.Success(model);
            var options = bindingContext.HttpContext.RequestServices.GetService<IOptions<MvcOptions>>();

            //if (_modelBinder is not null)
            //{
            //    if (!string.IsNullOrWhiteSpace(bindingContext.ModelName)
            //        && bindingContext.HttpContext.Request.Form.Any(w => w.Key == bindingContext.ModelName.ToSnakeCase()))
            //    {

            //    }

            //    await _modelBinder.BindModelAsync(bindingContext);
            //}
        }

        protected object GetFiles(HttpContext context, Type propertyType, string name)
        {
            var files = context.Request.Form.Files.Where(w => w.Name == name.ToSnakeCase());

            if (propertyType == typeof(IFormFileCollection))
            {
                var fileCollection = new FormFileCollection();
                fileCollection.AddRange(files);
                return fileCollection;
            }

            return IsCollectionOfFile(propertyType) ? files : files.FirstOrDefault();
        }

        protected (bool IsNotFound, object Value) GetValue(IValueProvider valueProvider
            , Type propertyType, string name)
        {
            //Get value
            var result = valueProvider.GetValue(name.ToSnakeCase());
            bool isNotFound = result != ValueProviderResult.None;

            //Convert string to original type.
            return (isNotFound, TypeConverterHelper.ConvertTo(result.FirstValue, propertyType));
        }

        protected bool IsFormFileType(PropertyInfo property)
            => FormFileTypes.Contains(property.PropertyType) || IsCollectionOfFile(property.PropertyType);

        protected bool IsCollectionOfFile(Type propertyType)
            => propertyType.GenericTypeArguments.Any(w => FormFileTypes.Contains(w));

        protected FormCollectionModelBinder CreateFormCollectionModelBinder(HttpContext context)
            => new(context.RequestServices.GetService<ILoggerFactory>());
    }

    //public class SnakeCaseModelBinderProvider : IModelBinderProvider
    //{
    //    public IModelBinder GetBinder(ModelBinderProviderContext context)
    //    {
    //        var metadata = context.Metadata;
    //        if (metadata.IsComplexType && !metadata.IsCollectionType)
    //        {
    //            //var parameters = GetParameterBinders(context);
    //            var propertyBinders = new Dictionary<ModelMetadata, IModelBinder>();
    //            //for (var i = 0; i < context.Metadata.Properties.Count; i++)
    //            //{
    //            //    var property = context.Metadata.Properties[i];
    //            //    propertyBinders.Add(property, context.CreateBinder(property));
    //            //}

    //            //context.CreateBinder()

    //            //var property = context.Metadata.Properties[1];
    //            //new DefaultModelMetadata()


    //            //for (var i = 0; i < context.Metadata.Properties.Count; i++)
    //            //{
    //            //    var property = context.Metadata.Properties[i];

    //            //    propertyBinders.Add(property, context.CreateBinder(property));
    //            //}

    //            var binder = new ComplexObjectModelBinderProvider().GetBinder(context);
    //            return binder is null ? null : new SnakeCaseModelBinder(binder);
    //        }
    //        return null;
    //    }

    //    private static IReadOnlyList<IModelBinder> GetParameterBinders(ModelBinderProviderContext context)
    //    {
    //        var boundConstructor = context.Metadata.BoundConstructor;
    //        if (boundConstructor is null)
    //        {
    //            return Array.Empty<IModelBinder>();
    //        }

    //        var parameterBinders = boundConstructor.BoundConstructorParameters!.Count == 0 ?
    //            Array.Empty<IModelBinder>() :
    //            new IModelBinder[boundConstructor.BoundConstructorParameters.Count];

    //        for (var i = 0; i < parameterBinders.Length; i++)
    //        {
    //            parameterBinders[i] = context.CreateBinder(boundConstructor.BoundConstructorParameters[i]);
    //        }

    //        return parameterBinders;
    //    }
    //}

    public class SnakeCaseFormValueProviderFactory : IValueProviderFactory
    {
        public async Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            if (!context.ActionContext.HttpContext.Request.HasFormContentType) return;

            foreach (var parameter in context.ActionContext.ActionDescriptor.Parameters)
            {
                if (!parameter.ParameterType.CustomAttributes.Any(w => w.AttributeType == typeof(SnakeCaseObjectAttribute)))
                {
                    continue;
                }

                //var properties = parameter.ParameterType.GetProperties();
                var form = await ReadFormAsync(context.ActionContext);
                var valueProvider = new FormValueProvider(BindingSource.Form
                    , CreateNewFormCollection(form)
                    , CultureInfo.CurrentCulture);

                context.ValueProviders.Add(valueProvider);
            }
        }

        private static async Task<IFormCollection> ReadFormAsync(ActionContext actionContext)
        {
            try
            {
                return await actionContext.HttpContext.Request.ReadFormAsync();
            }
            catch (InvalidDataException ex)
            {
                throw new ValueProviderException(string.Format("Failed to read the request form. {0}", ex.Message), ex);
            }
            catch (IOException ex)
            {
                throw new ValueProviderException(string.Format("Failed to read the request form. {0}", ex.Message), ex);
            }
        }

        private IFormCollection CreateNewFormCollection(IFormCollection form
            )
            //, IEnumerable<PropertyInfo> properties)
        {
            //var propertyNames = properties.Select(s => s.Name);
            var fields = form.ToDictionary(s => s.Key.SnakeCaseToPascalCase(), v => v.Value);
            var files = new FormFileCollection();

            foreach (var item in form.Files)
            {
                using (var stream = item.OpenReadStream())
                {
                    var file = new FormFile(stream, stream.Position, item.Length
                        , item.Name.SnakeCaseToPascalCase(), item.FileName)
                    {
                        Headers = item.Headers
                    };
                    files.Add(file);
                }
            }
            return new FormCollection(fields, files);
        }
    }
}