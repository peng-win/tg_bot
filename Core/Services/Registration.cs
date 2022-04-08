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
        
        public enum UserStatus
        {
            Active,
            NotActive,
            Blocked 
        }
        public async Task UserRegistration(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;
            string Name = null;
            string LastName = null;
            string Patronymic = null;
            string Phone = null;
            using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostreSQLConnection")))
            {
                try
                {
                    if (messageText == "/start")
                    {
                        
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
