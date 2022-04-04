using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Migrations.Migrations;
using FluentMigrator.Runner;

namespace Migrations.Extensions
{
    public static class MigrationManager
    {        
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                try
                {
                    databaseService.CreateDatabase("PizzaDeliveryDb");

                    migrationService.ListMigrations();
                    migrationService.MigrateDown(202203220001);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex);
                }
            }
            return host;
        }
    }
}