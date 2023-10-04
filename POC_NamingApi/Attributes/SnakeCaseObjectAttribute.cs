using Microsoft.AspNetCore.Mvc.ModelBinding;
using POC_NamingApi.Binders;

namespace POC_NamingApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SnakeCaseObjectAttribute : Attribute, IModelNameProvider, IBinderTypeProviderMetadata, ISnakeCaseAttribute
    {
        public string Name => "SnakeCaseModel";
        public Type BinderType { get; }
        //public Type BinderType => typeof(SnakeCaseModelBinder);
        public BindingSource BindingSource => BindingSource.Custom;
    }
}