using Microsoft.EntityFrameworkCore;
using ShippingService.Application.Interfaces;
using ShippingService.Application.Mappings;
using ShippingService.Application.Services;
using ShippingService.Domain.Interfaces;
using ShippingService.Infrastructure.Data;
using ShippingService.Infrastructure.Repositories;

namespace ShippingService.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddShippingDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuração do DbContext
            services.AddDbContext<ShippingDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("ShippingConnection"),
                    new MySqlServerVersion(new Version(8, 0, 11))));

            // Configuração do AutoMapper
            services.AddAutoMapper(typeof(ShippingProfile).Assembly);

            // Repositórios
            services.AddScoped<IShippingRepository, ShippingRepository>();
            services.AddScoped<ITrackingRepository, TrackingRepository>();

            // Serviços de Aplicação
            services.AddScoped<IShippingService, ShippingsService>();
            services.AddScoped<ITrackingService, TrackingService>();

            return services;
        }
    }
}