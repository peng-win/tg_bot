// See https://aka.ms/new-console-template for more information
using Migrations.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Migrations
{
	public class Program
	{
		public static void Main(string[] args)
        {
			var connectionString = "Host=localhost;Username=postgres;Password=4815162342;Database=PizzaDeliveryDb";
			var serviceProvider = CreateServices(connectionString);
			using var scope = serviceProvider.CreateScope();
			UpdateDatabase(scope.ServiceProvider);
        }

		private static IServiceProvider CreateServices(string connectionString)
        {
			return new ServiceCollection()
				.AddFluentMigratorCore()
				.ConfigureRunner(rb => rb
					.AddPostgres11_0()
					.WithGlobalConnectionString(connectionString)
					.ScanIn(typeof(UpdateTables_202204030005).Assembly).For.Migrations())
				.AddLogging(lb => lb.AddFluentMigratorConsole())
				.BuildServiceProvider(false);
        }

		private static void UpdateDatabase(IServiceProvider serviceProvider)
        { 
			var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
			runner.MigrateUp();
        }
	}
}