using app.Services;
using Data;
using Microsoft.EntityFrameworkCore;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {     
        services.AddHostedService<TimedHostedService>();

        services.AddControllers();
        /*
        services.AddDbContext<DataContext>(options =>
        {
            options
                .UseNpgsql(_configuration.GetConnectionString("DefaultConnection"),
                    assembly =>
                        assembly.MigrationsAssembly("Migrations"));


        });*/
        
    })
    .Build();

await host.StartAsync();

await host.WaitForShutdownAsync();