using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NamingApi.SnakeCase.ApiExplorer;
using NamingApi.SnakeCase.Helpers;
using NamingApi.SnakeCase.Json;
using System.Text.Json.Serialization;

namespace NamingApi.SnakeCase.Extensions
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