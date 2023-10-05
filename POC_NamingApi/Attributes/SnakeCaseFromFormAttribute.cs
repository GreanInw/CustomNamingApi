using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace POC_NamingApi.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class SnakeCaseFromFormAttribute : Attribute, IBindingSourceMetadata, IModelNameProvider, IFromFormMetadata, ISnakeCaseNamingAttribute
    {
        public SnakeCaseFromFormAttribute(string name) => Name = name.ToSnakeCase();

        public string Name { get; }
        public BindingSource BindingSource => BindingSource.Form;
    }
}