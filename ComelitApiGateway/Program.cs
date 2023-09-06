using ComelitApiGateway.Commons.Interfaces;
using ComelitApiGateway.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Choose whats did you prefer
builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(5000));
//builder.WebHost.ConfigureKestrel(options => options.ListenAnyIP(5000));

// Add services to the container.
builder.Services.AddSingleton<IComelitVedo, ComelitVedoService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, true);

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Disabled because is only in internal network
//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
