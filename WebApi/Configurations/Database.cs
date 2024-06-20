using Data.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Configurations;

public static class Database
{
     public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
            {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly("WebApi")));
            }
}