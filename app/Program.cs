using app.Services;
using Npgsql;
using Dapper;

using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=4815162342;Database=tgbotdb"))
{
    connection.Open();
    // connection.Query<string>("Select first_name from UserTbl");
    //connection.Execute("Insert into UserTbl (first_name, last_name, patronymic) values ('Иванов','Иван','Иванович');");
    var value = connection.Query<string>("Select first_name from UserTbl;");
    Console.WriteLine(value.First());
};

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {     
        services.AddHostedService<TimedHostedService>();

       
        
    })
    .Build();

await host.StartAsync();

await host.WaitForShutdownAsync();