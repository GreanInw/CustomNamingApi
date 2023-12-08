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
    internal class SnakeCaseQueryParametersApiDescriptionProvider : IApiDescriptionProvider
    {
        public SnakeCaseQueryParametersApiDescriptionProvider() : this(0) { }

        public SnakeCaseQueryParametersApiDescriptionProvider(int order)
        {
            Order = order;
        }

        public int Order { get; }

        public void OnProvidersExecuted(ApiDescriptionProviderContext context) { }

        public void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
            foreach (var request in context.Results)//Pre-request
            {
                foreach (var parameter in request.ParameterDescriptions)//Parameter of request
                {
                    bool hasSnakeCaseObjectAttr = SnakeCaseNamingHelper.HasSnakeCaseObjectAttribute(request);
                    bool hasSnakeCaseNameAttr = SnakeCaseNamingHelper.HasSnakeCaseNameAttribute(parameter);

                    if (!hasSnakeCaseObjectAttr && !hasSnakeCaseNameAttr)
                    {
                        continue;
                    }

                    string newName = hasSnakeCaseObjectAttr ? SnakeCaseBuilder.Build(parameter.Name)
                        : (!hasSnakeCaseNameAttr ? null
                            : SnakeCaseNamingHelper.GetSnakeCaseNameAttribute(parameter)?.Name);

                    parameter.Name = newName;
                }
            }
        }
    }
}