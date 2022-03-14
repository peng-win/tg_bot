using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.InlineQueryResults;
using Npgsql;

namespace app.Services
{
    public class CallMenu
    { /*
        private string _token;
        private string _connection;
        TelegramBotClient _client;
        public CallMenu(string connection, string token)
        {
            this._connection = connection;
            this._token = token;
        }
               
        static async Task CallMenuTask(Update update, CancellationToken cancellationToken, TelegramBotClient botClient)
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
                        InlineKeyboardMarkup inlineKeyboard = new(new[]
                        {

                            // first row
                            new []
                            {

                                InlineKeyboardButton.WithCallbackData(text: $"{s[2].ToString()} | {s[3].ToString()}", callbackData: "11"),
                            },
                        });
                        Message Message = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: s[1].ToString(),
                        replyMarkup: inlineKeyboard,
                        cancellationToken: cancellationToken);

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
            }
        }*/
    }
}
