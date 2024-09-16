﻿using AdvertBoard.Application.AppServices.Contexts.Adverts.Builders;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Services;
using AdvertBoard.Application.AppServices.Contexts.Categories.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Categories.Services;
using AdvertBoard.Application.AppServices.Contexts.Comments.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Comments.Services;
using AdvertBoard.Application.AppServices.Contexts.Images.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Images.Services;
using AdvertBoard.Application.AppServices.Contexts.Reviews.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Reviews.Services;
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
using Microsoft.Extensions.DependencyInjection;

namespace AdvertBoard.Infrastructure.ComponentRegistrar;

public static class ComponentRegistrar
{
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
}