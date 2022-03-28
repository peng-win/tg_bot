using FluentMigrator.Runner;
using MigrationsProject.Migrations;
using System.Reflection;

namespace MigrationsProject
{
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
					.WithGlobalConnectionString(Configuration.GetConnectionString("PostgreSQLConnection"))
					.ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());

			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}