using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Dapper;
using Telegram.Bot.Requests.Abstractions;
using Npgsql;
using Telegram.Bot.Types.ReplyMarkups;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Core.Services
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
            using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostreSQLConnection")))
            {
                try
                {
                    db.Open();

                    ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new[]
                    {                        
                        new [] 
                        {
                            new KeyboardButton("Пицца"),
                            new KeyboardButton("Напитки")
                        },
                        new[] 
                        {
                            new KeyboardButton("Десерты"),
                            new KeyboardButton("Закуски")
                        }
                    })
                    {
                        ResizeKeyboard = true
                    };

                    var limit = 1;
                    var offset = 0;
                    var sql = "";
                    var photo = "";
                    switch(messageText)
                    {
                        case "Пицца":
                            while (sql != null)
                            {
                                sql = db.Query<string>($"SELECT \"Product\" FROM \"Products\" WHERE \"TypeProduct\"='{messageText}' limit {limit} offset {offset}").SingleOrDefault();
                                photo = db.Query<string>($"SELECT \"Picture\" FROM \"Products\" WHERE \"TypeProduct\"='{messageText}' limit {limit} offset {offset}").SingleOrDefault();
                                Message Message = await botClient.SendPhotoAsync(
                                    chatId: chatId,
                                    photo: photo.ToString(),
                                    caption: sql.ToString(),
                                    replyMarkup: keyboard,
                                    cancellationToken: cancellationToken);
                                offset++;
                            }
                            break;

                        case "Напитки":
                            while (sql != null)
                            {
                                sql = db.Query<string>($"SELECT \"Product\" FROM \"Products\" WHERE \"TypeProduct\"='{messageText}' limit {limit} offset {offset}").SingleOrDefault();
                                photo = db.Query<string>($"SELECT \"Picture\" FROM \"Products\" WHERE \"TypeProduct\"='{messageText}' limit {limit} offset {offset}").SingleOrDefault();
                                Message Message = await botClient.SendPhotoAsync(
                                    chatId: chatId,
                                    photo: photo.ToString(),
                                    caption: sql.ToString(),
                                    replyMarkup: keyboard,
                                    cancellationToken: cancellationToken);
                                offset++;
                            }
                            break;

                        case "Десерты":
                            while (sql != null)
                            {
                                sql = db.Query<string>($"SELECT \"Product\" FROM \"Products\" WHERE \"TypeProduct\"='{messageText}' limit {limit} offset {offset}").SingleOrDefault();
                                photo = db.Query<string>($"SELECT \"Picture\" FROM \"Products\" WHERE \"TypeProduct\"='{messageText}' limit {limit} offset {offset}").SingleOrDefault();
                                Message Message = await botClient.SendPhotoAsync(
                                    chatId: chatId,
                                    photo: photo.ToString(),
                                    caption: sql.ToString(),
                                    replyMarkup: keyboard,
                                    cancellationToken: cancellationToken);
                                offset++;
                            }
                            break;

                        case "Закуски":
                            while (sql != null)
                            {
                                sql = db.Query<string>($"SELECT \"Product\" FROM \"Products\" WHERE \"TypeProduct\"='{messageText}' limit {limit} offset {offset}").SingleOrDefault();
                                photo = db.Query<string>($"SELECT \"Picture\" FROM \"Products\" WHERE \"TypeProduct\"='{messageText}' limit {limit} offset {offset}").SingleOrDefault();
                                Message Message = await botClient.SendPhotoAsync(
                                    chatId: chatId,
                                    photo: photo.ToString(),
                                    caption: sql.ToString(),
                                    replyMarkup: keyboard,
                                    cancellationToken: cancellationToken);
                                offset++;
                            }
                            break;

                        default:
                            while (sql != null)
                            {
                                sql = db.Query<string>($"SELECT \"Product\" FROM \"Products\" WHERE \"TypeProduct\"='{messageText}' limit {limit} offset {offset}").SingleOrDefault();
                                photo = db.Query<string>($"SELECT \"Picture\" FROM \"Products\" WHERE \"TypeProduct\"='{messageText}' limit {limit} offset {offset}").SingleOrDefault();
                                Message Message = await botClient.SendPhotoAsync(
                                    chatId: chatId,
                                    photo: photo.ToString(),
                                    caption: sql.ToString(),
                                    replyMarkup: keyboard,
                                    cancellationToken: cancellationToken);
                                offset++;
                            }
                            break;
                    }
                    /*
                    if (messageText == "Пицца")
                    {
                        while (sql != null)
                        {
                            sql = db.Query<string>($"SELECT \"Product\" FROM \"Products\" WHERE \"TypeProduct\"='{messageText}' limit {limit} offset {offset}").SingleOrDefault();

                            Message Message1 = await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: sql.ToString(),
                                replyMarkup: keyboard,
                                cancellationToken: cancellationToken);
                            offset++;
                        }
                    } else if (messageText == "Напитки")
                    {
                        while (sql != null)
                        {
                            sql = db.Query<string>($"SELECT \"Product\" FROM \"Products\" WHERE \"TypeProduct\"='{messageText}' limit {limit} offset {offset}").SingleOrDefault();

                            Message Message1 = await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: sql.ToString(),
                                replyMarkup: keyboard,
                                cancellationToken: cancellationToken);
                            offset++;
                        }
                    } else if (messageText == "Десерты")
                    {
                        while (sql != null)
                        {
                            sql = db.Query<string>($"SELECT \"Product\" FROM \"Products\" WHERE \"TypeProduct\"='{messageText}' limit {limit} offset {offset}").SingleOrDefault();

                            Message Message1 = await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: sql.ToString(),
                                replyMarkup: keyboard,
                                cancellationToken: cancellationToken);
                            offset++;
                        }
                    } else if (messageText == "Закуски")
                    {
                        while (sql != null)
                        {
                            sql = db.Query<string>($"SELECT \"Product\" FROM \"Products\" WHERE \"TypeProduct\"='{messageText}' limit {limit} offset {offset}").SingleOrDefault();

                            Message Message1 = await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: sql.ToString(),
                                replyMarkup: keyboard,
                                cancellationToken: cancellationToken);
                            offset++;
                        }
                    } else
                    {
                        while (sql != null)
                        {
                            sql = db.Query<string>($"SELECT \"Product\" FROM \"Products\" limit {limit} offset {offset}").SingleOrDefault();

                            Message Message1 = await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: sql.ToString(),
                                replyMarkup: keyboard,
                                cancellationToken: cancellationToken);
                            offset++;
                        }
                    }*/
                       
                        
                    
                    
                    
                    //dynamic sql = conn.Query("SELECT Product FROM Menu").ToString();
                    
                                             
                    db.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
            }
        }


    }
}