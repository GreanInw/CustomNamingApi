using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using NamingApi.SnakeCase.ApiExplorer;
using NamingApi.SnakeCase.Helpers;
using NamingApi.SnakeCase.Json;
using NamingApi.SnakeCase.Metadata;
using System.Text.Json.Serialization;

namespace NamingApi.SnakeCase.Extensions
{
    public static class SnakeCaseNamingExtensions
    {
        public static string ToSnakeCase(this string name) => SnakeCaseNamingHelper.ChangeTo(name);

        public static IServiceCollection AddSnakeCaseRequestResponse(this IServiceCollection services)
        {
            services.AddSnakeCaseRequest()
                .AddSnakeCaseJsonResponse()
                .AddIgnoreRequest();

            return services;
        }

        public static IServiceCollection AddSnakeCaseRequest(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.ModelMetadataDetailsProviders
                    .Insert(SnakeCaseMetadataProvider.DefaultOrder, new SnakeCaseMetadataProvider());
            });
            return services;
        }

        public static IServiceCollection AddSnakeCaseJsonResponse(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DictionaryKeyPolicy = SnakeCaseJsonNamingPolicy.Instance;
                options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseJsonNamingPolicy.Instance;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            return services;
        }

        public static IServiceCollection AddIgnoreRequest(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Transient<IApiDescriptionProvider, IgnoreRequestApiDescriptionProvider>());
            return services;
        }
    }
}