using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using NamingApi.SnakeCase.Builders;
using NamingApi.SnakeCase.Helpers;

namespace NamingApi.SnakeCase.ApiExplorer
{
    /// <summary>
    /// Rename parameter from <see cref="FromFormAttribute"/>, <see cref="FromQueryAttribute"/> 
    /// to naming snake_case.
    /// </summary>
    public class SnakeCaseQueryParametersApiDescriptionProvider : IApiDescriptionProvider
    {
        public int Order { get; }

        public void OnProvidersExecuted(ApiDescriptionProviderContext context) { }

        public void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
            foreach (var request in context.Results)//Pre-request
            {
                foreach (var parameter in request.ParameterDescriptions)//Parameter of request
                {
                    if (!SnakeCaseNamingHelper.IsSnakeCaseObject(request))
                    {
                        continue;
                    }
                    parameter.Name = SnakeCaseBuilder.Build(parameter.Name);
                }
            }
        }
    }
}