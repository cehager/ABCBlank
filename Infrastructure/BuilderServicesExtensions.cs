using Application.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class BuilderServicesExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContextFactory<ApplicationDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Infrastructure")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            //return services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            //b => b.MigrationsAssembly("Infrastructure")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IReadRepositoryAsync<,>), typeof(ReadRepositoryAsync<,>))
                .AddTransient(typeof(IWriteRepositoryAsync<,>), typeof(WriteRepositoryAsync<,>))
                .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }
    }
}
