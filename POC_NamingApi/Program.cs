using Microsoft.Extensions.DependencyInjection.Extensions;
using POC_NamingApi.Binders;
using System.Web.Http.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddNamingSnakeCaseRequest();
//builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IHttpActionSelector, SnakeCaseActionSelector>());

builder.Services.AddControllers(options =>
{
    //options.ModelBinderProviders.Insert(0, new SnakeCaseModelBinderProvider());
}).AddSnakeCaseJsonResponse();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();