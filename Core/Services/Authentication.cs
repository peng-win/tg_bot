using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Core.Interfaces;
using Data.Repositories;

namespace Core.Services
{
    public class Authentication : IAuthentication
    {
        private readonly IConfiguration _configuration;
        private readonly ICallMenu _callMenu;
        private readonly IRegistration _registration;
        private readonly IUserRepository _userRepository;
        
        public static string userName;
        public static string userLName;
        public static string userPhone;
        public static string userNickName;

        public static bool isAuthorization = false;

        public Authentication(IConfiguration configuration, ICallMenu callMenu, IRegistration registration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _callMenu = callMenu;
            _registration = registration;
            _userRepository = userRepository;
        }

        public async Task UserAuthentication(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            userNickName = update.Message.From.Username;

            foreach (string n in _userRepository.GetNickName())
            {
                if (n == userNickName)
                {
                    isAuthorization = true;
                    break;
                }
                else
                    isAuthorization = false;
            }

            if (isAuthorization == true)
            {
                await _callMenu.CallMenuTask(botClient, update, cancellationToken);                
            }
            else await _registration.UserRegistration(botClient, update, cancellationToken);
        }
    }
}
