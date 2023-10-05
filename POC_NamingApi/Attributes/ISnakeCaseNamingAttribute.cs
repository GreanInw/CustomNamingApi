using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace POC_NamingApi.Attributes
{
    public interface ISnakeCaseNamingAttribute
    {
        string Name { get; }
        BindingSource BindingSource { get; }
    }
}