using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using POC_NamingApi.Helpers;
using System.ComponentModel.Design;
using System.Web.Http.Controllers;

namespace POC_NamingApi.Binders
{
    public class SnakeCaseModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            //var modelName = bindingContext.ModelName;

            //// Try to fetch the value of the argument by name
            //var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            //if (valueProviderResult == ValueProviderResult.None)
            //{
            //    return Task.CompletedTask;
            //}

            //bindingContext.ModelState.SetModelValue()


            return Task.CompletedTask;
        }
    }

    public class SnakeCaseModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            //new BinderTypeModelBinder()

            return null;
        }
    }

    public class SnakeCaseActionSelector : ApiControllerActionSelector
    {
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            var newUri = CreateNewUri(controllerContext.Request.RequestUri
                , controllerContext.Request.GetQueryNameValuePairs());

            controllerContext.Request.RequestUri = newUri;

            return base.SelectAction(controllerContext);
        }

        private Uri CreateNewUri(Uri requestUri, IEnumerable<KeyValuePair<string, string>> queryPairs)
        {
            var currentQuery = requestUri.Query;
            var newQuery = ConvertQueryToCamelCase(queryPairs);
            return new Uri(requestUri.ToString().Replace(currentQuery, newQuery));
        }

        private static string ConvertQueryToCamelCase(IEnumerable<KeyValuePair<string, string>> queryPairs)
        {
            queryPairs = queryPairs.Select(x =>
            {
                return new KeyValuePair<string, string>(SnakeCaseNamingHelper.ConvertToCamelCase(x.Key), x.Value);
            });
            return "?" + queryPairs
                .Select(x => string.Format("{0}={1}", x.Key, x.Value))
                .Aggregate((x, y) => x + "&" + y);
        }
    }
}
