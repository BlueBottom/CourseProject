using System.IdentityModel.Tokens.Jwt;
using AdvertBoard.Contracts.Contexts.Adverts;
using AdvertBoard.Contracts.Contexts.Adverts.Responses;
using AdvertBoard.Contracts.Contexts.Images;
using AdvertBoard.Hosts.Api.Controllers;
using AdvertBoard.Hosts.Api.Middlewares;
using AdvertBoard.Infrastructure.ComponentRegistrar;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo{Title = "AdvertBoard API", Version = "v1"});

    var docTypeMarkers = new[]
    {
        typeof(AdvertResponse),
        typeof(AdvertController)
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
    
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new ()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = """
                      JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: Bearer 1safsfsdfdfd
                      """,
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme,
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDependencies();
builder.Services.AddDatabase(builder.Configuration);

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
builder.Services.AddAuthenticationWithJwtToken(builder.Configuration);
builder.Services.AddAuthorization();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UsePathBase("/api");
app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();