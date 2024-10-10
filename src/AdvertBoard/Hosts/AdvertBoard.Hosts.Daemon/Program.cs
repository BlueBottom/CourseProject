using AdvertBoard.Application.AppServices.Contexts.Email.Services;
using AdvertBoard.Application.AppServices.Contexts.Reviews.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Users.Services.Rating;
using AdvertBoard.Contracts.Contexts.Reviews.Events;
using AdvertBoard.Contracts.Contexts.Users.Events;
using AdvertBoard.Hosts.Daemon.Consumers;
using AdvertBoard.Hosts.Daemon.Extensions;
using AdvertBoard.Infrastructure.ComponentRegistrar;
using AdvertBoard.Infrastructure.ComponentRegistrar.MapProfiles;
using AdvertBoard.Infrastructure.DataAccess.Contexts.Reviews.Repositories;
using AdvertBoard.Infrastructure.DataAccess.Contexts.Users.Repositories;
using AdvertBoard.Infrastructure.Repository;
using MassTransit;
using Serilog;

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
            });
            bus.AddConsumer<ReviewCreatedConsumer>();
            bus.AddConsumer<UserRegisteredConsumer>();
        });

        builder.Services.AddScoped<IUserRatingService, UserRatingService>();
        builder.Services.AddScoped<IUserEmailService, UserEmailService>();
        
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