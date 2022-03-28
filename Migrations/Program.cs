// See https://aka.ms/new-console-template for more information
using Migrations.Extensions;
using Migrations.Migrations;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Migrations
{
	public class Program
	{
	}
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<DapperContext>();
			services.AddSingleton<Database>();

			services.AddLogging(c => c.AddFluentMigratorConsole())
				.AddFluentMigratorCore()
				.ConfigureRunner(c => c.AddSqlServer2012()
					.WithGlobalConnectionString(Configuration.GetConnectionString("Host=localhost;Username=postgres;Password=4815162342;Database=PizzaDeliveryDb"))
					.ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());

		}
	}
}

/*
IHost _host;

static IHostBuilder ConfigureWebHostDefaults(this Microsoft.Extensions.Hosting.IHostBuilder builder, Action<Microsoft.AspNetCore.Hosting.IWebHostBuilder> configure);

static void Main(string[] args, IHost _host)
{
CreateHostBuilder(args)
.Build()
.MigrateDatabase()
.Run(); 
}

static IHostBuilder CreateHostBuilder(string[] args) =>
Host.CreateDefaultBuilder(args)
.ConfigureWebHostDefaults(webBuilder =>
{
webBuilder.UseStartup<MigrationManager>();
});*/