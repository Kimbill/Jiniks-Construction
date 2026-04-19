using Jiniks.Data;
using Jiniks.Infrastructure;
using Jiniks.Services.Implementations;
using Jiniks.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jiniks.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }

    public static IServiceCollection AddCloudinaryIntegration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
        services.AddScoped<ICloudinaryService, CloudinaryService>();

        return services;
    }

    public static IServiceCollection AddInfrastructureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.Configure<PhotoSettings>(configuration.GetSection("PhotoSettings"));

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IHeroSlideService, HeroSlideService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IContactService, ContactService>();

        return services;
    }
}
