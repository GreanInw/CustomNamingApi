using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using POC_NamingApi.Attributes;
using System.Globalization;
using System.Text;

namespace POC_NamingApi.ModelBinders
{
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
                //var form = await ReadFormAsync(context.ActionContext);
                //var valueProvider = new FormValueProvider(BindingSource.Form
                //    , CreateNewFormCollection(form)
                //    , CultureInfo.CurrentCulture);

                //context.ValueProviders.Add(valueProvider);

                var form = await context.ActionContext.HttpContext.Request.ReadFormAsync();
                var valueProvider = new SnakeCaseFormValueBinderProvider(BindingSource.Form
                    , form, CultureInfo.CurrentCulture);
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

        private IFormCollection CreateNewFormCollection(IFormCollection form)
        {
            var fields = form.ToDictionary(s => s.Key.SnakeCaseToPascalCase(), v => v.Value);
            var files = GetFiles(form);
            return new FormCollection(fields, files);
        }

        private FormFileCollection GetFiles(IFormCollection form)
        {
            var files = new FormFileCollection();
            foreach (var item in form.Files)
            {
                string newName = item.Name.SnakeCaseToPascalCase();

                var fileBuffering = new FileBufferingReadStream(item.OpenReadStream(), 1024 * 1024);


                //using var stream = item.OpenReadStream();
                var file = new FormFile(fileBuffering, fileBuffering.Position, item.Length, newName, item.FileName)
                {
                    Headers = item.Headers,
                    ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = $"\"{newName}\"",
                        FileName = item.FileName,
                    }.ToString(),
                };

                files.Add(file);
            }

            return files;
        }

    }

    public class SnakeCaseFormValueBinderProvider : FormValueProvider, IValueProvider
    {
        public SnakeCaseFormValueBinderProvider(BindingSource bindingSource, IFormCollection values, CultureInfo culture)
            : base(bindingSource, values, culture)
        {
        }

        public override bool ContainsPrefix(string prefix)
        {
            return base.ContainsPrefix(ConvertToSnakeCase(prefix));
        }

        public override IDictionary<string, string> GetKeysFromPrefix(string prefix)
        {
            return base.GetKeysFromPrefix(prefix.ToSnakeCase());
        }

        public override ValueProviderResult GetValue(string key)
        {
            return base.GetValue(ConvertToSnakeCase(key));
        }

        private string ConvertToSnakeCase(string value)
        {
            if (!value.Contains('.'))
            {
                return value.ToSnakeCase();
            }

            var names = value.Split('.');
            var builder = new StringBuilder();
            foreach (var name in names)
            {
                builder.AppendFormat("{0}{1}", builder.Length == 0 ? "" : ".", name.ToSnakeCase());
            }

            return builder.ToString();
        }
    }
}