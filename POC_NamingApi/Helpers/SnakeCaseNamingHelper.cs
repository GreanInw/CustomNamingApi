using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Newtonsoft.Json.Serialization;
using POC_NamingApi.Attributes;

namespace POC_NamingApi.Helpers
{
    public class SnakeCaseNamingHelper
    {
        internal static SnakeCaseNamingStrategy SnakeCaseNaming => new();

        public static string ChangeTo(string name) => SnakeCaseNaming.GetPropertyName(name, false);

        public static bool IsSnakeCaseObject(ApiDescription description)
        {
            if (description is null) throw new ArgumentNullException(nameof(description));

            var customAttributes = description.ActionDescriptor.Parameters
                .SelectMany(s => s.ParameterType.CustomAttributes);

            return customAttributes.Any(w => w.AttributeType == typeof(SnakeCaseObjectAttribute));
        }

        public static bool IsSnakeCaseAttribute(ApiParameterDescription parameter)
        {
            if (parameter is null) throw new ArgumentNullException(nameof(parameter));

            if (parameter.ModelMetadata is not DefaultModelMetadata defaultModel)
            {
                return false;
            }

            var allowTypes = new[]
            {
                typeof(SnakeCaseFromFormAttribute), typeof(SnakeCaseFromQueryAttribute)
            };

            return defaultModel.Attributes.Attributes.Any(w => allowTypes.Contains(w.GetType()));
        }

        public static ISnakeCaseAttribute GetSnakeCaseAttribute(ApiParameterDescription parameter)
        {
            if (parameter is null) throw new ArgumentNullException(nameof(parameter));

            if (parameter.ModelMetadata is not DefaultModelMetadata defaultModel)
            {
                return null;
            }
            return defaultModel.Attributes.Attributes.FirstOrDefault(s
                => s.GetType().GetInterface(nameof(ISnakeCaseAttribute), true) is not null) as ISnakeCaseAttribute;
        }

        public static string ConvertToCamelCase(string source)
        {
            var parts = source.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            return $"{parts.First().ToLower()}{string.Join("", parts.Skip(1).Select(ConvertToCapital))}";
        }

        public static string ConvertToCapital(string source) 
            => string.Format("{0}{1}", char.ToUpper(source[0]), source.Substring(1).ToLower());
    }
}