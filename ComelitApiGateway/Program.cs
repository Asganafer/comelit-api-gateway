using ComelitApiGateway.Commons.Exceptions;
using ComelitApiGateway.Commons.Interfaces;
using ComelitApiGateway.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Get environment variable injected from docker
builder.Configuration.AddEnvironmentVariables();

if (String.IsNullOrEmpty(builder.Configuration["VEDO_KEY"]))
{
    throw new GeneralException("VEDO_KEY is not set");
}

if (String.IsNullOrEmpty(builder.Configuration["VEDO_URL"]))
{
    throw new GeneralException("VEDO_URL is not set");
}

//Choose whats did you prefer
#if DEBUG
    builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(5000));
#else
builder.WebHost.ConfigureKestrel(options => options.ListenAnyIP(5000));
#endif

// Add services to the container.
builder.Services.AddSingleton<IComelitVedo, ComelitVedoService>();
builder.Services.AddSingleton<IVedoEventDispatcher, VedoEventDispatcher>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, true);

});

var app = builder.Build();

if (app.Environment.IsDevelopment() || Convert.ToBoolean(builder.Configuration["ENABLE_SWAGGER"]) == true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Disabled because is only in internal network
//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
