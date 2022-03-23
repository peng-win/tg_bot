using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

using Telegram.Bot.Requests.Abstractions;
using Npgsql;
using Telegram.Bot.Types.ReplyMarkups;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Core
{
    public class CallMenu : ICallMenu
    {
        private readonly IConfiguration _configuration;

        public CallMenu(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task CallMenuTask(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostreSQLConnection")))
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
                            replyMarkup: inlineKeyboard,
                            cancellationToken: cancellationToken);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
            }
        }


    }
}