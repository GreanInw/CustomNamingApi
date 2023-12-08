using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using NamingApi.SnakeCase.Attributes;
using NamingApi.SnakeCase.Builders;
using NamingApi.SnakeCase.Helpers;

namespace NamingApi.SnakeCase.Metadata
{
    public class SnakeCaseMetadataProvider : IBindingMetadataProvider
    {
        public const int DefaultOrder = 0;

        public void CreateBindingMetadata(BindingMetadataProviderContext context)
        {
            bool isSnakeCaseObjectAttr = SnakeCaseNamingHelper.HasSnakeCaseObjectAttribute(context.Key);
            bool isSnakeCaseNameAttr = SnakeCaseNamingHelper.HasSnakeCaseNameAttribute(context.Key.PropertyInfo);

            if (!isSnakeCaseObjectAttr && !isSnakeCaseNameAttr)
            {
                return;
            }

            var nameAttribute = !isSnakeCaseNameAttr ? null
                : (SnakeCaseNameAttribute)context.Attributes
                    .FirstOrDefault(w => w.GetType() == typeof(SnakeCaseNameAttribute));

            context.BindingMetadata.BinderModelName = isSnakeCaseObjectAttr
                ? SnakeCaseBuilder.Build(context.Key.Name)
                : nameAttribute.Name;
        }
    }
}