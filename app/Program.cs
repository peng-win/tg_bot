using app.Services;
using Core;
using Data.Repositories;
using Data;
using Core.Interfaces;
using Core.Services;

var builder = WebApplication.CreateBuilder(args);

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<TimedHostedService>();
        services.AddSingleton<ICallMenu, CallMenu>();
        services.AddSingleton<IRegistration, Registration>();
        services.AddSingleton<IProductRepository, ProductRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IAuthentication, Authentication>();

    })
    .Build();

await host.StartAsync();

await host.WaitForShutdownAsync();