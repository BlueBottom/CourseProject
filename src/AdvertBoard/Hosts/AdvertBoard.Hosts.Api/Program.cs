using AdvertBoard.Contracts.Contexts.Adverts;
using AdvertBoard.Contracts.Contexts.Images;
using AdvertBoard.Hosts.Api.Controllers;
using AdvertBoard.Infrastructure.ComponentRegistrar;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo{Title = "AdvertBoard API", Version = "v1"});

    var docTypeMarkers = new[]
    {
        typeof(AdvertDto),
        typeof(AdvertController),
        typeof(ImageDto),
        typeof(ImageController)
    };

    foreach (var marker in docTypeMarkers)
    {
        var xmlFile = $"{marker.Assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }
    }
});
builder.Services.AddDependencies();
builder.Services.AddDatabase(builder.Configuration);
// TODO: перенести в ComponentRegistrar
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5138") 
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
// app.UseAuthorization();
app.MapControllers();

app.Run();