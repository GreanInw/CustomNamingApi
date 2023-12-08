using Microsoft.AspNetCore.Mvc.ModelBinding;
using NamingApi.SnakeCase.Metadata;

namespace NamingApi.SnakeCase.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SnakeCaseObjectAttribute : Attribute, IBinderTypeProviderMetadata
    {
        public Type BinderType => typeof(SnakeCaseMetadataProvider);
        public BindingSource BindingSource => BindingSource.Custom;
    }
}