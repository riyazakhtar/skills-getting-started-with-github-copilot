using Application.Repositores;
using Azure.Core;
using Azure.Identity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(
            !string.IsNullOrEmpty(connectionString) ? connectionString : "Host=127.0.0.1;"
        );

        // if connection string does not have a password use entra id auth
        // otherwise just use connection string
        if (string.IsNullOrEmpty(new NpgsqlConnectionStringBuilder(connectionString).Password))
        {
            dataSourceBuilder.UsePeriodicPasswordProvider(
                async (_, ct) =>
                {
                    DefaultAzureCredential credential = new();
                    TokenRequestContext ctx =
                        new(["https://ossrdbms-aad.database.windows.net/.default"]);
                    AccessToken tokenResponse = await credential.GetTokenAsync(ctx, ct);
                    return tokenResponse.Token;
                },
                TimeSpan.FromHours(4),
                TimeSpan.FromSeconds(10)
            );
        }

        var dataSource = dataSourceBuilder.Build();

        // add database
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(dataSource, x => x.MigrationsHistoryTable("_migrations"));
            // PostgreSQL world tends towards snake_case naming
            options.UseSnakeCaseNamingConvention();

            // Seed data, both methods use the same logic
            // Read more https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding
            if (configuration.GetValue<bool>("seed"))
            {
                options.UseSeeding(
                    (db, _) =>
                    {
                        TestDataSeeder.Seed((ApplicationDbContext)db);
                    }
                );
                options.UseAsyncSeeding(
                    async (db, _, cancellationToken) =>
                    {
                        TestDataSeeder.Seed((ApplicationDbContext)db);
                        await Task.CompletedTask;
                    }
                );
            }
        });

        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IProductTypeRepository, ProductTypeRepository>();

        return services;
    }
}
