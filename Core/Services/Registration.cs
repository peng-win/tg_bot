using System.Data;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Core.Interfaces;
using Core.Models;
using Npgsql;
using Dapper;

namespace Core.Services
{
    public class Registration : IRegistration
    {
        private readonly IConfiguration _configuration;
        private readonly ICallMenu _callMenu;

        public Registration(IConfiguration configuration, ICallMenu callMenu)
        {
            _configuration = configuration;
            _callMenu = callMenu;
        }

        public async Task UserRegistration(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;
            
            if (messageText.ToLower() == "/start")
            {
                await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "Добро пожаловать!",
                    cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "Введите фамилию, имя, отчество и номер телефона через пробел",
                    cancellationToken: cancellationToken);
            }

            await UserAnswer(botClient, update, cancellationToken);
            //await _callMenu.CallMenuTask(botClient, update, cancellationToken);

        }

        public async Task UserAnswer(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;

            if (update.Type == UpdateType.Message)
            {
                var answer = update.Message.Text;
                if (answer.ToLower() != null)
                {
                    string[] info = answer.Split(' ');                    

                    
                    if (info[0].Length < 2 || info[1].Length < 2 || info[2].Length < 2 || info[3].Length < 11 || info[1] == null)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Неверный ввод, повторите попытку",
                            cancellationToken: cancellationToken);
                        return;
                    }
                    else
                    {
                        foreach (Char d in info[3])
                        {
                            if (Char.IsDigit(d) == false)
                            {
                                await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Неверный ввод номера телефона, повторите попытку",
                                cancellationToken: cancellationToken);
                                return;
                            }
                        }
                        string lastName = info[0];
                        string name = info[1];
                        string patronymic = info[2];
                        string phone = info[3];
                        DateTime regDate = DateTime.Now;
                        Guid id = Guid.NewGuid();
                        string userStatus = Enums.UserStatus.Active.ToString();
                        
                        using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostreSQLConnection")))
                        {
                            try
                            {
                                db.Query<string>($"INSERT INTO \"User\" VALUES ('{id}','{name}','{lastName}','{patronymic}','{regDate}','{phone}','{userStatus}')");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex);
                            }
                        }
                        
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"{name}, добро пожаловать!",
                            cancellationToken: cancellationToken);
                        /*
                        if (name != null && lastName != null && patronymic != null && phone != null)
                        {
                            await _callMenu.CallMenuTask(botClient, update, cancellationToken);
                        }*/
                    }
                    return;
                }
                return;
            }
        }
    }
}
