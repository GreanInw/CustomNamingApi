using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using POC_NamingApi.Attributes;
using System.Reflection;

namespace POC_NamingApi.ModelBinders
{
    public class SnakeCaseMetadataProvider : IBindingMetadataProvider
    {
        private ILogger _logger;

        public SnakeCaseMetadataProvider()
        {
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = loggerFactory?.CreateLogger("SnakeCaseMetadataProvider");
        }

        public void CreateBindingMetadata(BindingMetadataProviderContext context)
        {
            //bool isSnakeCaseRequest = context.Key.ContainerType == typeof(SamplePostRequest)
            //    || context.Key.ContainerType?.DeclaringType == typeof(SamplePostRequest);
            //_logger?.LogInformation($"=============> 0-ContainerType : {context.Key.ContainerType}, {context.Key.Name}");

            if (IsSnakeCaseObject(context.Key))
            {
                context.BindingMetadata.BinderModelName = context.Key.Name.ToSnakeCase();
            }
        }

        private bool IsSnakeCaseObject(ModelMetadataIdentity modelMetadata)
        {
            if (modelMetadata.ContainerType is null)
            {
                return false;
            }

            var attribute = GetAttribute(modelMetadata.ContainerType);
            var declareAttribute = modelMetadata.ContainerType.DeclaringType is null
                ? null : GetAttribute(modelMetadata.ContainerType.DeclaringType);

            return attribute is not null || declareAttribute is not null;
        }

        private SnakeCaseObjectAttribute GetAttribute(Type type)
            => type.GetCustomAttribute<SnakeCaseObjectAttribute>();
    }
}