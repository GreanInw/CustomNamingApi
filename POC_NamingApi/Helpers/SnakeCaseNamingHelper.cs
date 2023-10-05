using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Newtonsoft.Json.Serialization;
using POC_NamingApi.Attributes;
using System.Reflection;

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

        public static bool IsSnakeCaseObject(Type type) => GetSnakeCaseObjectAttribute(type) is not null;

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

        public static ISnakeCaseNamingAttribute GetSnakeCaseAttribute(ApiParameterDescription parameter)
        {
            if (parameter is null) throw new ArgumentNullException(nameof(parameter));

            if (parameter.ModelMetadata is not DefaultModelMetadata defaultModel)
            {
                return null;
            }
            return defaultModel.Attributes.Attributes.FirstOrDefault(s
                => s.GetType().GetInterface(nameof(ISnakeCaseNamingAttribute), true) is not null) as ISnakeCaseNamingAttribute;
        }

        public static SnakeCaseObjectAttribute GetSnakeCaseObjectAttribute(Type type)
            => type.GetCustomAttribute<SnakeCaseObjectAttribute>();

    }
}