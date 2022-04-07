using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Telegram.Bot;
using Telegram.Bot.Types;
using Core.Interfaces;

namespace Core.Services
{
    public class Registration : IRegistration
    {
        private readonly IConfiguration _configuration;

        public Registration(IConfiguration configuration)
        {
            _configuration = configuration;                       
        }
        /*
        public enum UserStatus
        {
            Active,
            NotActive,
            Blocked 
        }*/
        public async Task UserRegistration(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;
            using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostreSQLConnection")))
            {
                try
                {
                    do
                    {
                        Message Message = await botClient.SendTextMessageAsync(
                                                    chatId: chatId,
                                                    text: "Введите имя",
                                                    cancellationToken: cancellationToken);
                    }
                    while (messageText.Length < 1 && messageText.Length > 100);
                        
                        
                    
                    var Name = messageText;

                    do
                    {
                        Message Message = await botClient.SendTextMessageAsync(
                                                    chatId: chatId,
                                                    text: "Введите фамилию",
                                                    cancellationToken: cancellationToken);
                    }
                    while (messageText.Length < 1 && messageText.Length > 100);
                    var LastName = messageText;
                    do
                    {
                        Message Message = await botClient.SendTextMessageAsync(
                                                    chatId: chatId,
                                                    text: "Введите отчество",
                                                    cancellationToken: cancellationToken);
                    }
                    while (messageText.Length < 1 && messageText.Length > 100);
                    var Patronymic = messageText;
                    do
                    {
                        Message Message = await botClient.SendTextMessageAsync(
                                                    chatId: chatId,
                                                    text: "Введите номер телефона",
                                                    cancellationToken: cancellationToken);
                    }
                    while (messageText.Length < 1 && messageText.Length > 100);
                    var Phone = messageText;


                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
            }
        }        
    }
}
