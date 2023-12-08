using NamingApi.SnakeCase.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSnakeCaseRequestResponse();

//builder.Services.AddControllers(options =>
//{
//    //options.Filters.Add<SnakeCaseActionFilter>(0);
//    options.ModelMetadataDetailsProviders.Insert(0, new SnakeCaseMetadataProvider());

//    //options.ModelBinderProviders.Insert(0, new SnakeCaseModelBinderProvider());
//    //options.ValueProviderFactories.Insert(0, new SnakeCaseFormValueProviderFactory());

//});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<SnakeCaseRequestMiddleware>();
app.MapControllers();
app.Run();