using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace POC_NamingApi.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class SnakeCaseFromQueryAttribute : Attribute, IBindingSourceMetadata, IModelNameProvider, IFromQueryMetadata, ISnakeCaseAttribute
    {
        public SnakeCaseFromQueryAttribute(string name) => Name = name.ToSnakeCase();

        public string Name { get; }
        public BindingSource BindingSource => BindingSource.Query;
    }
}