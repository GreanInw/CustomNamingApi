using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace POC_NamingApi.Attributes
{
    public interface ISnakeCaseAttribute
    {
        string Name { get; }
        BindingSource BindingSource { get; }
    }
}