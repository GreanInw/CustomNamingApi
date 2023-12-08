using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Primitives;

namespace POC_NamingApi.Middlewares
{
    public abstract class InternalBaseMiddleware
    {
        protected RequestDelegate Next;

        protected InternalBaseMiddleware(RequestDelegate next) => Next = next;

        public abstract Task InvokeAsync(HttpContext context);
    }

    //public class SnakeCaseRequestMiddleware : InternalBaseMiddleware
    //{
    //    public SnakeCaseRequestMiddleware(RequestDelegate next) : base(next) { }

    //    public override async Task InvokeAsync(HttpContext context)
    //    {
    //        if (context.Request.Form.Any())
    //        {
    //            var fields = context.Request.Form.ToDictionary(s => s.Key.SnakeCaseToPascalCase(), v => v.Value);
    //            var files = new FormFileCollection();

    //            foreach (var item in context.Request.Form.Files)
    //            {
    //                using (var stream = item.OpenReadStream())
    //                {
    //                    var file = new FormFile(stream, stream.Position, item.Length
    //                        , item.Name.SnakeCaseToPascalCase(), item.FileName)
    //                    {
    //                        Headers = item.Headers
    //                    };
    //                    files.Add(file);
    //                }
    //            }
                
    //            context.Request.Form = new FormCollection(fields, files);
    //        }

    //        await Next(context);
    //    }
    //}
}