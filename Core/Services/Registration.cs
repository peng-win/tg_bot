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
        
        
        public async Task UserRegistration(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;
            /*
            switch (messageText)
            {
                case "/start":
                    
                    break;
            }*/
                
            if (messageText == "/start")
            {
               
                await UserAnswer(botClient, update, cancellationToken);
            }
            
        }        

        public async Task UserAnswer(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;
            Message message = await botClient.SendTextMessageAsync(
                       chatId: chatId,
                       text: "Введите имя, фамилию, отчество и номер телефона через пробел",
                       cancellationToken: cancellationToken);
            
            var messageText = update.Message.Chat;

            message = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: messageText.ToString(),
                        cancellationToken: cancellationToken);

        }
    }
}
