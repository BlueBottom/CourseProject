using System.Text;
using AdvertBoard.Application.AppServices.Authorization.Handlers;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Builders;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Services;
using AdvertBoard.Application.AppServices.Contexts.Authentication.Services;
using AdvertBoard.Application.AppServices.Contexts.Categories.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Categories.Services;
using AdvertBoard.Application.AppServices.Contexts.Comments.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Comments.Services;
using AdvertBoard.Application.AppServices.Contexts.Images.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Images.Services;
using AdvertBoard.Application.AppServices.Contexts.Reviews.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Reviews.Services;
using AdvertBoard.Application.AppServices.Contexts.Users.Builders;
using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Users.Services;
using AdvertBoard.Infrastructure.ComponentRegistrar.MapProfiles;
using AdvertBoard.Infrastructure.DataAccess.Contexts.Adverts.Repositories;
using AdvertBoard.Infrastructure.DataAccess.Contexts.Categories.Repositories;
using AdvertBoard.Infrastructure.DataAccess.Contexts.Comments.Repositories;
using AdvertBoard.Infrastructure.DataAccess.Contexts.Images.Repositories;
using AdvertBoard.Infrastructure.DataAccess.Contexts.Reviews.Repositories;
using AdvertBoard.Infrastructure.DataAccess.Contexts.Users.Repositories;
using AdvertBoard.Infrastructure.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace AdvertBoard.Infrastructure.ComponentRegistrar;

public static class ComponentRegistrar
{
    /// <summary>
    /// Добавляет методы расширения.
    /// </summary>
    /// <param name="serviceCollection">Коллекция сервисов.</param>
    /// <returns>Ioc.</returns>
    public static IServiceCollection AddDependencies(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddServices()
            .AddRepositories()
            .AddBuilders()
            .AddMapper();
    }

    private static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAdvertService, AdvertService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IImageService, ImageService>();
        serviceCollection.AddScoped<IReviewService, ReviewService>();
        serviceCollection.AddScoped<ICommentService, CommentService>();
        serviceCollection.AddScoped<ICategoryService, CategoryService>();
        serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();

        //Handlers для работы с ресурсной авторизацией.
        serviceCollection.AddScoped<IAuthorizationHandler, IsAdvertOwnerHandler>();
        serviceCollection.AddScoped<IAuthorizationHandler, IsCurrentUserHandler>();
        serviceCollection.AddScoped<IAuthorizationHandler, IsAdminHandler>();
        
        return serviceCollection;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAdvertRepository, AdvertRepository>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<IImageRepository, ImageRepository>();
        serviceCollection.AddScoped<IReviewRepository, ReviewRepository>();
        serviceCollection.AddScoped<ICommentRepository, CommentRepository>();
        serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();

        serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return serviceCollection;
    }

    private static IServiceCollection AddBuilders(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAdvertSpecificationBuilder, AdvertSpecificationBuilder>();
        serviceCollection.AddScoped<IUserSpecificationBuilder, UserSpecificationBuilder>();

        return serviceCollection;
    }
    private static IServiceCollection AddMapper(this IServiceCollection serviceCollection)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AdvertMapProfile>();
            cfg.AddProfile<UserMapProfile>();
            cfg.AddProfile<ImageMapProfile>();
        });
        config.AssertConfigurationIsValid();

        serviceCollection.AddSingleton<IMapper>(new Mapper(config));

        return serviceCollection;
    }
    
    /// <summary>
    /// Настраивает аутентификацию.
    /// </summary>
    /// <param name="serviceCollection">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация.</param>
    /// <returns></returns>
    public static IServiceCollection AddAuthenticationWithJwtToken(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        
        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // указывает, будет ли валидироваться издатель при валидации токена
                    ValidateIssuer = true,
                    // строка, представляющая издателя
                    ValidIssuer = configuration["Jwt:Issuer"],
                    // будет ли валидироваться потребитель токена
                    ValidateAudience = true,
                    // установка потребителя токена
                    ValidAudience = configuration["Jwt:Audience"],
                    // будет ли валидироваться время существования
                    ValidateLifetime = true,
                    // установка ключа безопасности
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                    // валидация ключа безопасности
                    ValidateIssuerSigningKey = true,
                };
            });

        return serviceCollection;
    }
}