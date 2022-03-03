using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.InlineQueryResults;
using Npgsql;

using Dapper;

namespace app.Services
{
  
    public class TimedHostedService : IHostedService
    {
        private readonly ILogger<TimedHostedService> _logger;
        private readonly IConfiguration _configuration;
        private string[] args;

        public TimedHostedService(ILogger<TimedHostedService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

          
            var token = _configuration.GetValue<string>("token");

            var bot = new TelegramBotClient(token);


            using var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions { AllowedUpdates = { } };

            bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken: cts.Token);


           // var me = await bot.GetMeAsync();
            return Task.CompletedTask;
        }
        
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            /*
            if (update.Type != Telegram.Bot.Types.Enums.UpdateType.Message)
                return;
            if (update.Message!.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
            /*
            

            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Вы сказали: " + messageText,
              
                cancellationToken: cancellationToken);*/

            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;
            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=4815162342;Database=tgbotdb"))
            {
                //connection.Open();
                //connection.Query<string>("Select first_name from UserTbl");
                //connection.Execute("Insert into UserTbl (first_name, last_name, patronymic) values ('Иван','Иванов','Иванович');");
                /*var value = connection.Query<string>("Select first_name from UserTbl;");
                Console.WriteLine(value.First());*/

                try
                {

                    connection.Open();
                    NpgsqlCommand npgsqlCommand = connection.CreateCommand();
                    npgsqlCommand.CommandText = "SELECT * FROM menutbl";
                    NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader();
                    List<string[]> data = new List<string[]>();

                    ReplyKeyboardMarkup keyboard = new(new[]
                        {
                            new KeyboardButton[] { "Меню" },
                        })
                    {
                        ResizeKeyboard = true
                    };
                    /*InlineKeyboardButton inlineKeyboardButton = new InlineKeyboardButton();
                    InlineKeyboardMarkup inlineKeyboard = new(new[]
                        {
                            new InlineKeyboardButton("Текст для первой кнопки","callback1"),
                            new InlineKeyboardButton("Текст второй кнопки","callback2"),
                        });
                    )*/

                    while (npgsqlDataReader.Read())
                    {
                        data.Add(new string[5]);
                        data[data.Count - 1][0] = npgsqlDataReader[0].ToString();
                        data[data.Count - 1][1] = npgsqlDataReader[1].ToString();
                        data[data.Count - 1][2] = npgsqlDataReader[2].ToString();
                        data[data.Count - 1][3] = npgsqlDataReader[3].ToString();
                        data[data.Count - 1][4] = npgsqlDataReader[4].ToString();
                    }
                    npgsqlDataReader.Close();
                    connection.Close();
                    foreach (string[] s in data)
                    {
                        Message Message = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: s[1].ToString() + " | " + s[2].ToString() + " | " + s[3].ToString(),
                        replyMarkup: keyboard,
                        cancellationToken: cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
            };
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(errorMessage);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Hosted Service is stopping.");            

            return Task.CompletedTask;
        }

        
    }
    
}
