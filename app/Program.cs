using app.Services;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {     
        services.AddHostedService<TimedHostedService>();
    })
    .Build();

await host.StartAsync();

await host.WaitForShutdownAsync();

