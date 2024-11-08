using Clinica_Dental;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurar la cadena de conexión
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Registrar MydDbContext con SQL Server
            services.AddDbContext<MydDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Registrar otros servicios aquí si es necesario

            return services;
        }
    }
}
