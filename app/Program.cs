using app.Services;
using Core;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
var builder = WebApplication.CreateBuilder(args);

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<TimedHostedService>();
        services.AddSingleton<ICallMenu, CallMenu>();
    })
    .Build();

await host.StartAsync();

await host.WaitForShutdownAsync();