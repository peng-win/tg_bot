using MigrationsProject;
using MigrationsProject.Extensions;

	CreateHostBuilder(args)
		.Build()
		.MigrateDatabase()
		.Run();


static IHostBuilder CreateHostBuilder(string[] args) =>
	Host.CreateDefaultBuilder(args)
		.ConfigureWebHostDefaults(webBuilder =>
		{
			webBuilder.UseStartup<Startup>();
		});