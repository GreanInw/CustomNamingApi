using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using NamingApi.SnakeCase.Attributes;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace NamingApi.SnakeCase.Helpers
{
    public class SnakeCaseNamingHelper
    {
        internal static SnakeCaseNamingStrategy SnakeCaseNaming => new();

        public static string ChangeTo(string name) => SnakeCaseNaming.GetPropertyName(name, false);

        public static bool HasSnakeCaseObjectAttribute(ApiDescription description)
        {
            if (description is null) throw new ArgumentNullException(nameof(description));

            var customAttributes = description.ActionDescriptor.Parameters
                .SelectMany(s => s.ParameterType.CustomAttributes);

            return customAttributes.Any(w => w.AttributeType == typeof(SnakeCaseObjectAttribute));
        }

        public static bool HasSnakeCaseNameAttribute(ApiParameterDescription parameterDescription)
            => GetSnakeCaseNameAttribute(parameterDescription) is not null;

        public static bool HasSnakeCaseObjectAttribute(Type type)
            => type is not null && GetSnakeCaseObjectAttribute(type) is not null;

        public static bool HasSnakeCaseObjectAttribute(ModelMetadataIdentity metadataIdentity)
            => GetSnakeCaseObjectAttribute(metadataIdentity) is not null;

        public static bool HasSnakeCaseNameAttribute(Type type)
            => type is not null && GetSnakeCaseNameAttribute(type) is not null;

        public static bool HasSnakeCaseNameAttribute(PropertyInfo property)
            => GetSnakeCaseNameAttribute(property) is not null;

        public static SnakeCaseObjectAttribute GetSnakeCaseObjectAttribute(Type type)
            => type?.GetCustomAttribute<SnakeCaseObjectAttribute>();

        public static SnakeCaseObjectAttribute GetSnakeCaseObjectAttribute(ModelMetadataIdentity metadataIdentity)
            => metadataIdentity.ContainerType is null ? null : GetSnakeCaseObjectAttribute(metadataIdentity.ContainerType);

        public static SnakeCaseNameAttribute GetSnakeCaseNameAttribute(Type type)
            => type?.GetCustomAttribute<SnakeCaseNameAttribute>();

        public static SnakeCaseNameAttribute GetSnakeCaseNameAttribute(ApiParameterDescription parameterDescription)
        {
            if (parameterDescription is null || parameterDescription.ModelMetadata is null
                || parameterDescription.ModelMetadata is not DefaultModelMetadata defaultModel)
            {
                return null;
            }

            var attribute = defaultModel.Attributes.Attributes
                .FirstOrDefault(w => w.GetType() == typeof(SnakeCaseNameAttribute));

            return attribute is SnakeCaseNameAttribute newAttribute ? newAttribute : null;
        }

        public static SnakeCaseNameAttribute GetSnakeCaseNameAttribute(PropertyInfo property)
            => property?.GetCustomAttribute<SnakeCaseNameAttribute>();
    }
}