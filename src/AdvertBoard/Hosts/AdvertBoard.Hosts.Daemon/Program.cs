using AdvertBoard.Application.AppServices.Contexts.Accounts.Services;
using AdvertBoard.Application.AppServices.Contexts.Accounts.Validators.BusinessLogic;
using AdvertBoard.Application.AppServices.Contexts.Email.Services;
using AdvertBoard.Application.AppServices.Contexts.Reviews.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Users.Services.Rating;
using AdvertBoard.Application.AppServices.Notifications.Services;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Accounts.Events;
using AdvertBoard.Contracts.Contexts.Accounts.Requests;
using AdvertBoard.Contracts.Contexts.Reviews.Events;
using AdvertBoard.Hosts.Daemon.Consumers;
using AdvertBoard.Hosts.Daemon.Extensions;
using AdvertBoard.Infrastructure.ComponentRegistrar;
using AdvertBoard.Infrastructure.ComponentRegistrar.MapProfiles;
using AdvertBoard.Infrastructure.DataAccess.Contexts.Reviews.Repositories;
using AdvertBoard.Infrastructure.DataAccess.Contexts.Users.Repositories;
using AdvertBoard.Infrastructure.Repository;
using MassTransit;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Enums;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace AdvertBoard.Hosts.Daemon;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddMassTransit(bus =>
        {
            bus.UsingRabbitMq((ctx, configurator) =>
            {
                configurator.Host(builder.Configuration.GetConnectionString("RabbitMq"));
                configurator.AddEndpoint<ReviewStatusUpdatedEvent, ReviewCreatedConsumer>(ctx);
                configurator.AddEndpoint<UserRegisteredEvent, UserRegisteredConsumer>(ctx);
                configurator.AddEndpoint<AskRecoveryPasswordCodeEvent, RecoveryPasswordCodeSentConsumer>(ctx);
                configurator.AddEndpoint<PasswordRecoveredEvent, PasswordRecoveredConsumer>(ctx);
            });
            bus.AddConsumer<ReviewCreatedConsumer>();
            bus.AddConsumer<RecoveryPasswordCodeSentConsumer>();
            bus.AddConsumer<UserRegisteredConsumer>();
            bus.AddConsumer<PasswordRecoveredConsumer>();
        });

        builder.Services.AddScoped<IUserRatingService, UserRatingService>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<INotificationService, NotificationService>();
    
        builder.Services.AddScoped<BusinessLogicAbstractValidator<LoginUserRequest>, LoginUserValidator>();
        builder.Services.AddScoped<BusinessLogicAbstractValidator<RegisterUserRequest>, RegisterUserValidator>();
        builder.Services.AddScoped<BusinessLogicAbstractValidator<AskRecoveryPasswordCodeRequest>, AskRecoveryPasswordCodeValidator>();
    
        builder.Services.AddFluentValidationAutoValidation(configuration =>
        {
            configuration.ValidationStrategy = ValidationStrategy.All;
            configuration.DisableBuiltInModelValidation = false;
            configuration.EnableBodyBindingSourceAutomaticValidation = true;
            configuration.EnableCustomBindingSourceAutomaticValidation = true;
            configuration.EnableFormBindingSourceAutomaticValidation = true;
            configuration.EnablePathBindingSourceAutomaticValidation = true;
            configuration.EnableQueryBindingSourceAutomaticValidation = true;
        });
    
        builder.Services.AddMemoryCache();
        builder.Services.AddRedis(builder.Configuration);
        
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        builder.Services.AddAutoMapper(typeof(UserMapProfile));
        
        builder.Services.AddDatabase(builder.Configuration);
        
        builder.Services.AddSerilog(config =>
        {
            config.ReadFrom.Configuration(builder.Configuration)
                .Enrich.WithEnvironmentName();
        });

        var host = builder.Build();
        host.Run();
    }
}