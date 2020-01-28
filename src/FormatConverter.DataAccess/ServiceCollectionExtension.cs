using System;
using FormatConverter.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FormatConverter.DataAccess
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSqLiteDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDbContext<DataContext>(builder =>
                {
                    builder
                        .EnableSensitiveDataLogging(true)
                        .UseSqlite(configuration.GetConnectionString("SQLiteDatabase"),
                            x => x.MigrationsAssembly("FormatConverter.DataAccess.Migrations")
                                .CommandTimeout((int) TimeSpan.FromMinutes(10).TotalSeconds));
                })
                .AddScoped<IDbRepository, DbRepository>(
                    provider => new DbRepository(provider.GetRequiredService<DataContext>()));
        }
    }
}