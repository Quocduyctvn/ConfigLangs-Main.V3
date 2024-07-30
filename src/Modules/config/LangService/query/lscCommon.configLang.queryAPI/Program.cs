using Asp.Versioning;
using lscCommon.configLang.queryAPI.DependencyInjection.Extensions;
using lscCommon.configLang.queryAPI.DependencyInjection.Options;
using lscCommon.configLang.queryAPI.Middleware;
using lscCommon.configLang.queryApplication.DependencyInjection.Extensions;
using lscCommon.configLang.queryPersistence.DependencyInjection.Extensions;
using lscCommon.configLang.queryPresentation.Abstractions;
using lscCommon.configLang.queryPresentation.Common;
using lscCommon.configLang.queryPresentation.DependencyInjection.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddPresentation();
builder.Services.AddPersistence(builder.Configuration);
var serviceName = "LangService";
//register controllers
builder.Services.AddControllers().AddApplicationPart(Assembly.GetExecutingAssembly());
//register api configuration
builder.Services.AddSingleton(new ApiConfig { Name = serviceName });
//Configure swagger
builder.Services.ConfigureOptions<SwaggerConfigureOptions>();

//Configure api versioning
builder.Services.AddApiVersioning(
        options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("x-api-version"),
                new QueryStringApiVersionReader());
        })
    .AddMvc()
    .AddApiExplorer(
        options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.UseApiLayerSwagger();
app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseAuthorization();
app.MapPresentation();
app.Run();