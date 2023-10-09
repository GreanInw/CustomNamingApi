using POC_NamingApi.ModelBinders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddNamingSnakeCaseRequest();

builder.Services.AddControllers(options =>
{
    //options.ModelMetadataDetailsProviders.Insert(0, new SnakeCaseBindingMetadataProvider());
    //options.ModelBinderProviders.Insert(0, new SnakeCaseModelBinderProvider());

    //options.Filters.Add<SnakeCaseActionFilter>(0);

    options.ValueProviderFactories.Insert(0, new SnakeCaseFormValueProviderFactory());

}).AddSnakeCaseJsonResponse();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<SnakeCaseRequestMiddleware>();
app.MapControllers();
app.Run();