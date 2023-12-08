using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using NamingApi.SnakeCase.Extensions;
using NamingApi.SnakeCase.Helpers;

namespace NamingApi.SnakeCase.Metadata
{
    public class SnakeCaseMetadataProvider : IBindingMetadataProvider
    {
        public void CreateBindingMetadata(BindingMetadataProviderContext context)
        {
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

            var isDeclareSnakeCaseObject = modelMetadata.ContainerType.DeclaringType is null
                ? false : SnakeCaseNamingHelper.IsSnakeCaseObject(modelMetadata.ContainerType.DeclaringType);

            return SnakeCaseNamingHelper.IsSnakeCaseObject(modelMetadata.ContainerType) 
                || isDeclareSnakeCaseObject;
        }
    }
}