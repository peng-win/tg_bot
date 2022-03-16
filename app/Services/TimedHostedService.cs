using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.InlineQueryResults;
using Npgsql;
using Core;
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


            return Task.CompletedTask;
        }
        
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                await CallMenuTask(botClient, update, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
            
        }

        public static void Registration(string chatId, string username)
        {
            try
            {
                var DB = new NpgsqlConnection("Host=localhost;Username=postgres;Password=4815162342;Database=tgbotdb");
                DB.Open();
                NpgsqlCommand regcmd = DB.CreateCommand();
                regcmd.CommandText = "INSERT INTO RegUsers VALUES(@chatId, @username)";
                regcmd.Parameters.AddWithValue("@chatId", chatId);
                regcmd.Parameters.AddWithValue("@username", username);
                regcmd.ExecuteNonQuery();
                DB.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
            }
        }

        public async Task CallMenuTask(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;
            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=4815162342;Database=tgbotdb"))
            {
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

                    while (npgsqlDataReader.Read())
                    {
                        data.Add(new string[10]);
                        data[data.Count - 1][0] = npgsqlDataReader[0].ToString();
                        data[data.Count - 1][1] = npgsqlDataReader[1].ToString();
                        data[data.Count - 1][2] = npgsqlDataReader[2].ToString();
                        data[data.Count - 1][3] = npgsqlDataReader[3].ToString();
                        data[data.Count - 1][4] = npgsqlDataReader[4].ToString();
                        data[data.Count - 1][5] = npgsqlDataReader[5].ToString();
                        data[data.Count - 1][6] = npgsqlDataReader[6].ToString();
                        data[data.Count - 1][7] = npgsqlDataReader[7].ToString();
                        data[data.Count - 1][8] = npgsqlDataReader[8].ToString();
                        data[data.Count - 1][9] = npgsqlDataReader[9].ToString();
                    }
                    npgsqlDataReader.Close();
                    connection.Close();
                    foreach (string[] s in data)
                    {                       
                        
                            InlineKeyboardMarkup inlineKeyboard = new(new[]
                            {
                                new []
                                {
                                    InlineKeyboardButton.WithCallbackData(text: $"{s[2].ToString()} | {s[3].ToString()}", callbackData: "11"),
                                    InlineKeyboardButton.WithCallbackData(text: $"{s[4].ToString()} | {s[5].ToString()}", callbackData: "11"),
                                    InlineKeyboardButton.WithCallbackData(text: $"{s[6].ToString()} | {s[7].ToString()}", callbackData: "11"),
                                },
                            });     
                        /*
                        Message Message1 = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: s[1].ToString(),
                            replyMarkup: inlineKeyboard,
                            cancellationToken: cancellationToken);*/
                        Message Message = await botClient.SendPhotoAsync(
                            chatId: chatId,
                            photo: s[9].ToString(),
                            caption: s[1].ToString(),
                            replyMarkup : inlineKeyboard,
                            cancellationToken: cancellationToken);
                       }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
            }
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
