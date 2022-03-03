using app.Services;
using Npgsql;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Dapper;
/*
using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=4815162342;Database=tgbotdb"))
{
    connection.Open();
    //connection.Query<string>("Select first_name from UserTbl");
    //connection.Execute("Insert into UserTbl (first_name, last_name, patronymic) values ('Иван','Иванов','Иванович');");
    var value = connection.Query<string>("Select first_name from UserTbl;");
    Console.WriteLine(value.First());

    try
    {
        connection.Open();
        NpgsqlCommand npgsqlCommand = connection.CreateCommand();
        npgsqlCommand.CommandText = "SELECT * FROM [menutbl]";
        List<string[]> data = new List<string[]>();

        foreach (string[] s in data)
        {
            Message message = await 
        }
    }

};*/

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {     
        services.AddHostedService<TimedHostedService>();

       
        
    })
    .Build();

await host.StartAsync();

await host.WaitForShutdownAsync();