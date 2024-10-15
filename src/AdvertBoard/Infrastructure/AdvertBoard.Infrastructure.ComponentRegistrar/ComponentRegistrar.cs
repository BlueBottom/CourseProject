using System.Text;
using AdvertBoard.Application.AppServices.Authorization.Handlers;
using AdvertBoard.Application.AppServices.Contexts.Accounts.Services;
using AdvertBoard.Application.AppServices.Contexts.Accounts.Validators.BusinessLogic;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Builders;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Services;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Validators.BusinessLogic;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Validators.Requests;
using AdvertBoard.Application.AppServices.Contexts.Categories.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Categories.Services;
using AdvertBoard.Application.AppServices.Contexts.Categories.Validators.BusinessLogic;
using AdvertBoard.Application.AppServices.Contexts.Comments.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Comments.Services;
using AdvertBoard.Application.AppServices.Contexts.Comments.Validators.BusinessLogic;
using AdvertBoard.Application.AppServices.Contexts.Images.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Images.Services;
using AdvertBoard.Application.AppServices.Contexts.Images.Validators.BusinessLogic;
using AdvertBoard.Application.AppServices.Contexts.Reviews.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Reviews.Services;
using AdvertBoard.Application.AppServices.Contexts.Reviews.Validators.BusinessLogic;
using AdvertBoard.Application.AppServices.Contexts.Users.Builders;
using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Users.Services;
using AdvertBoard.Application.AppServices.Contexts.Users.Services.Rating;
using AdvertBoard.Application.AppServices.Contexts.Users.Validators.BusinessLogic;
using AdvertBoard.Application.AppServices.Notifications.Services;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Accounts.Requests;
using AdvertBoard.Contracts.Contexts.Adverts.Requests;
using AdvertBoard.Contracts.Contexts.Categories.Requests;
using AdvertBoard.Contracts.Contexts.Comments.Requests;
using AdvertBoard.Contracts.Contexts.Images.Requests;
using AdvertBoard.Contracts.Contexts.Reviews.Requests;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using AdvertBoard.Infrastructure.ComponentRegistrar.MapProfiles;
using AdvertBoard.Infrastructure.DataAccess.Contexts.Adverts.Repositories;
using AdvertBoard.Infrastructure.DataAccess.Contexts.Categories.Repositories;
using AdvertBoard.Infrastructure.DataAccess.Contexts.Comments.Repositories;
using AdvertBoard.Infrastructure.DataAccess.Contexts.Images.Repositories;
using AdvertBoard.Infrastructure.DataAccess.Contexts.Reviews.Repositories;
using AdvertBoard.Infrastructure.DataAccess.Contexts.Users.Repositories;
using AdvertBoard.Infrastructure.Repository;
using AdvertBoard.Infrastructure.Repository.Relational;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Enums;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace AdvertBoard.Infrastructure.ComponentRegistrar;

public static class ComponentRegistrar
{
    /// <summary>
    /// Добавляет методы расширения.
    /// </summary>
    /// <param name="serviceCollection">Коллекция сервисов.</param>
    /// <returns>IoC.</returns>
    public static IServiceCollection AddDependencies(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddServices()
            .AddRepositories()
            .AddBuilders()
            .AddMapper()
            .AddFluentValidation();
    }

    private static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAdvertService, AdvertService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IImageService, ImageService>();
        serviceCollection.AddScoped<IReviewService, ReviewService>();
        serviceCollection.AddScoped<ICommentService, CommentService>();
        serviceCollection.AddScoped<ICategoryService, CategoryService>();
        serviceCollection.AddScoped<IUserRatingService, UserRatingService>();
        serviceCollection.AddScoped<IAccountService, AccountService>();
        //serviceCollection.AddScoped<IUserPasswordService, UserPasswordService>();

        //Handlers для работы с ресурсной авторизацией.
        serviceCollection.AddScoped<IAuthorizationHandler, IsAdvertOwnerHandler>();
        serviceCollection.AddScoped<IAuthorizationHandler, IsCurrentUserHandler>();
        serviceCollection.AddScoped<IAuthorizationHandler, IsReviewOwnerHandler>();
        serviceCollection.AddScoped<IAuthorizationHandler, IsCommentOwnerHandler>();
        serviceCollection.AddScoped<IAuthorizationHandler, IsAdminHandler>();
        
        //Notifications
        serviceCollection.AddTransient<INotificationService, NotificationService>();
        
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
        serviceCollection.AddScoped(typeof(IRelationalRepository<>), typeof(RelationalRepository<>));

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
            cfg.AddProfile<CategoryMapProfile>();
            cfg.AddProfile<ReviewMapProfile>();
            cfg.AddProfile<CommentMapProfile>();
        });
        config.AssertConfigurationIsValid();

        serviceCollection.AddSingleton<IMapper>(new Mapper(config));

        return serviceCollection;
    }
    
    /// <summary>
    /// Подключить пакеты для работы с FluentValidation.
    /// </summary>
    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        // Добавление в DI валидаторов для всех requests
        services.AddValidatorsFromAssemblyContaining<CreateAdvertRequestValidator>(filter: service => 
            service.ValidatorType.BaseType?.GetGenericTypeDefinition() != typeof(BusinessLogicAbstractValidator<>));
        
        // Валидация бизнес логики объявлений
        services.AddScoped<BusinessLogicAbstractValidator<CreateAdvertRequest>, CreateAdvertValidator>();
        
        // Валидация бизнес логики аутентификации
        services.AddScoped<BusinessLogicAbstractValidator<LoginUserRequest>, LoginUserValidator>();
        services.AddScoped<BusinessLogicAbstractValidator<RegisterUserRequest>, RegisterUserValidator>();
        services.AddScoped<BusinessLogicAbstractValidator<RecoverPasswordWithCodeRequest>, RecoverPasswordWithCodeValidator>();
        services.AddScoped<BusinessLogicAbstractValidator<AskRecoveryPasswordCodeRequest>, AskRecoveryPasswordCodeValidator>();
        
        // Валидация бизнес логики категорий
        services.AddScoped<BusinessLogicAbstractValidator<CreateCategoryRequest>, CreateCategoryValidator>();

        // Валидация бизнес логики комментариев
        services.AddScoped<BusinessLogicAbstractValidator<CreateCommentRequest>, CreateCommentValidator>();
        services.AddScoped<BusinessLogicAbstractValidator<GetAllCommentsRequest>, GetAllCommentsValidator>();
        
        // Валидация бизнес логики изображений
        services.AddScoped<BusinessLogicAbstractValidator<CreateImageRequest>, CreateImageValidator>();
        
        // Валидация бизнес логики отзывов
        services.AddScoped<BusinessLogicAbstractValidator<CreateReviewRequest>, CreateReviewValidator>();
        services.AddScoped<BusinessLogicAbstractValidator<GetAllReviewsRequest>, GetAllReviewsValidator>();
        
        //бизнес логики пользователя
        services.AddScoped<BusinessLogicAbstractValidator<UpdateUserRequest>, UpdateUserValidator>();
        
            services.AddFluentValidationAutoValidation(configuration =>
            {
                configuration.ValidationStrategy = ValidationStrategy.All;
                configuration.DisableBuiltInModelValidation = false;
                configuration.EnableBodyBindingSourceAutomaticValidation = true;
                configuration.EnableCustomBindingSourceAutomaticValidation = true;
                configuration.EnableFormBindingSourceAutomaticValidation = true;
                configuration.EnablePathBindingSourceAutomaticValidation = true;
                configuration.EnableQueryBindingSourceAutomaticValidation = true;
            });

        return services;
    }
    
    /// <summary>
    /// Настраивает аутентификацию.
    /// </summary>
    /// <param name="serviceCollection">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация.</param>
    /// <returns>Коллекцию сервисов.</returns>
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
                    ValidateLifetime = false,
                    // установка ключа безопасности
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                    // валидация ключа безопасности
                    ValidateIssuerSigningKey = true,
                };
            });

        return serviceCollection;
    }

    /// <summary>
    /// Подключает редис.
    /// </summary>
    /// <param name="serviceCollection">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация.</param>
    /// <returns>Коллекцию сервисов.</returns>
    public static IServiceCollection AddRedis(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddStackExchangeRedisCache(options =>
        {
            options.InstanceName = "advert_";
            options.Configuration = configuration.GetConnectionString("Redis");
        });

        return serviceCollection;
    }
}