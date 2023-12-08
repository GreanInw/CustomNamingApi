using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace NamingApi.SnakeCase.ApiExplorer
{
    /// <summary>
    /// Ignore request parameter with <see cref="BindNeverAttribute"/>.
    /// </summary>
    internal class IgnoreRequestApiDescriptionProvider : IApiDescriptionProvider
    {
        public IgnoreRequestApiDescriptionProvider() : this(0) { }

        public IgnoreRequestApiDescriptionProvider(int order)
        {
            Order = order;
        }

        public int Order { get; }

        public void OnProvidersExecuted(ApiDescriptionProviderContext context) { }

        public void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
            foreach (var request in context.Results)//Per-request
            {
                static bool predicate(ApiParameterDescription w)
                    => w.ModelMetadata is not null
                        && w.ModelMetadata is DefaultModelMetadata model
                        && model.Attributes.Attributes.Any(a => a.GetType() == typeof(BindNeverAttribute));

                var ignoreParameters = request.ParameterDescriptions
                    .Where(predicate).ToList();

                foreach (var item in ignoreParameters)
                {
                    request.ParameterDescriptions.Remove(item);
                }
            }
        }
    }
}