using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using POC_NamingApi.Helpers;
using POC_NamingApi.NamingPolicy;
using System.Text.Json.Serialization;

namespace POC_NamingApi.Extensions
{
    public static class SnakeCaseNamingExtensions
    {
        public static string ToSnakeCase(this string name) => SnakeCaseNamingHelper.ChangeTo(name);

        public static IMvcBuilder AddSnakeCaseJsonResponse(this IMvcBuilder builder)
        {
            builder.AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DictionaryKeyPolicy = SnakeCaseJsonNamingPolicy.Instance;
                options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseJsonNamingPolicy.Instance;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            return builder;
        }

        public static IServiceCollection AddNamingSnakeCaseRequest(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Transient<IApiDescriptionProvider, SnakeCaseQueryParametersApiDescriptionProvider>());
            return services;
        }
    }
}