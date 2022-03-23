using app.Services;
using Core;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Migrations;
using Migrations.Migrations;
using Migrations.Extensions;
using FluentMigrator.Runner;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

/*
static IHostBuilder CreateHostBuilder(string[] args) =>
	Host.CreateDefaultBuilder(args)
		.ConfigureWebHostDefaults(webBuilder =>
		{
			webBuilder.UseStartup<Database>();
		});

CreateHostBuilder(args).Build().MigrateDatabase().Run();
var service = new ServiceCollection()
               .AddFluentMigratorCore()
               .ConfigureRunner(rb => rb
                   .AddPostgres11_0()
               );*/

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<TimedHostedService>();
        services.AddSingleton<ICallMenu, CallMenu>();

        services.AddSingleton<DapperContext>();
        services.AddSingleton<Database>();
       
        services.AddLogging(c => c.AddFluentMigratorConsole())
            .AddFluentMigratorCore()
            .ConfigureRunner(c => c.AddPostgres11_0()
                .WithGlobalConnectionString(builder.Configuration.GetConnectionString("PostreSQLConnection"))
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());
    })
    .Build();

await host.StartAsync();

await host.WaitForShutdownAsync();