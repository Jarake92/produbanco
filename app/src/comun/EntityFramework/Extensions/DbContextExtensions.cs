using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace shared.comun.EntityFramework.Extensions;

public static class DbContextExtensions
{
    public static void AddCustomDbContext<TContext>(
        this IServiceCollection services, 
        IConfiguration configuration,
        string dbConnection)
        where TContext : DbContext
    {
        var useInMemoryDatabase = configuration.GetValue<bool>("UseInMemoryDatabase");

        if (useInMemoryDatabase)
        {
            services.AddDbContext<TContext>(options => options.UseInMemoryDatabase("ClientesDb"));

            using var scope = services.BuildServiceProvider().CreateScope();
            var servicesScope = scope.ServiceProvider;
            var context = servicesScope.GetRequiredService<TContext>();
            context.Database.EnsureCreated();
        }
        else
        {
            services.AddDbContext<TContext>(options => options.UseSqlServer(dbConnection));
        }
    }
}