using System.Data;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Core.Interfaces;
using Core.Models;
using Data.Repositories;
using Npgsql;
using Dapper;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Concurrent;

namespace Core.Services
{
    public class Registration : IRegistration
    {
        private readonly IConfiguration _configuration;

        public Registration(IConfiguration configuration,  IUserRepository userRepository)
        {
            _configuration = configuration;
        }

        public async Task UserRegistration(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var messageText = update.Message.Text;
            

            if (messageText.ToLower() == "/start" || Authentication.isAuthorization == false)
            {
                
                await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "Введите фамилию, имя и номер телефона через пробел",
                    cancellationToken: cancellationToken);
                
            }
            await UserAnswer(botClient, update, cancellationToken);
        }        
        private async Task UserAnswer(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {      
            var chatId = update.Message.Chat.Id;
            string[] info = update.Message.Text.Split(' ');

            if (info[0].Length < 2 || info[1].Length < 2 || info[2].Length < 11 || info[1] == null)
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Неверный ввод, повторите попытку",
                    cancellationToken: cancellationToken);
                return;
            }
            else
            {
                Authentication.userLName = info[0];
                Authentication.userName = info[1];
                Authentication.userPhone = info[2].Replace("+7", "8");
                Authentication.userNickName = update.Message.From.Username;
                DateTime regDate = DateTime.Now;
                Guid id = Guid.NewGuid();
                string userStatus = Enums.UserStatus.Active.ToString();

                foreach (Char d in Authentication.userPhone)
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

                using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQLConnection")))
                {
                    try
                    {
                        db.Query<string>($"INSERT INTO \"User\" VALUES ('{id}','{Authentication.userName}','{Authentication.userLName}','{regDate}','{Authentication.userPhone}','{userStatus}','{Authentication.userNickName}')");
                        Authentication.isAuthorization = true;
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"{Authentication.userName}, добро пожаловать!",
                            cancellationToken: cancellationToken);
                    }
                    catch 
                    {
                        Authentication.isAuthorization = true;
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"{Authentication.userName}, с возвращением!",
                            cancellationToken: cancellationToken);
                    }
                }
            }            
        }
    }
}
