using app.Services;
using Core;
using Core.Interfaces;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;
var builder = WebApplication.CreateBuilder(args);

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<TimedHostedService>();
        services.AddSingleton<ICallMenu, CallMenu>();
        services.AddSingleton<IRegistration, Registration>();
    })
    .Build();

await host.StartAsync();

await host.WaitForShutdownAsync();