using Microsoft.AspNetCore.Mvc.ApiExplorer;
using NamingApi.SnakeCase.Attributes;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace NamingApi.SnakeCase.Helpers
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

        public static bool IsSnakeCaseObject(Type type)
            => type is not null && GetSnakeCaseObjectAttribute(type) is not null;

        public static SnakeCaseObjectAttribute GetSnakeCaseObjectAttribute(Type type)
            => type.GetCustomAttribute<SnakeCaseObjectAttribute>();
    }
}