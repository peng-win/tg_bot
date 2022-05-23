using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Core.Services;
using Dapper;
using Npgsql;
using Telegram.Bot.Types.ReplyMarkups;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using Data.Repositories;
using System.Linq;

namespace Core.Services
{
    public class CallMenu : ICallMenu
    {
        private readonly IConfiguration _configuration;
        private readonly IProductRepository _productRepository;
        private readonly IRegistration _registration;

        public CallMenu(IConfiguration configuration, IProductRepository productRepository, IRegistration registration)
        {
            _configuration = configuration;
            _productRepository = productRepository;
            _registration = registration;
        }
        
        public async Task CallMenuTask(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var messageText = update.Message.Text;
            var chatId = update.Message.Chat.Id;

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

            await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Выберите пункт меню: ",
                    replyMarkup: keyboard,
                    cancellationToken: cancellationToken);            

            await SelectMenuItem(botClient, update, cancellationToken);
        }

        public async Task SelectMenuItem(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var messageText = update.Message.Text;

            try
            {
                switch (messageText)
                {
                    case "Пицца":
                        if (Authentication.isAuthorization == false)
                        {
                            await _registration.UserRegistration(botClient, update, cancellationToken);
                        }
                        else await GetPizza(botClient, update, cancellationToken);
                        break;

                    case "Напитки":
                        if (Authentication.isAuthorization == false)
                        {
                            await _registration.UserRegistration(botClient, update, cancellationToken);
                        }
                        else await GetDrinks(botClient, update, cancellationToken);
                        break;

                    case "Десерты":
                        if (Authentication.isAuthorization == false)
                        {
                            await _registration.UserRegistration(botClient, update, cancellationToken);
                        }
                        else await GetDesserts(botClient, update, cancellationToken);
                        break;

                    case "Закуски":
                        if (Authentication.isAuthorization == false)
                        {
                            await _registration.UserRegistration(botClient, update, cancellationToken);
                        }
                        else await GetSnacks(botClient, update, cancellationToken);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
        public async Task GetPizza(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;
            
            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Подробнее", callbackData: "11"),
                },
            });

            foreach (string s in _productRepository.GetPizza())
            {
                Message Message = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: s,
                        replyMarkup: inlineKeyboard,
                        cancellationToken: cancellationToken);
            }
        }
        public async Task GetDesserts(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var messageText = update.Message.Text;

            var chatId = update.Message.Chat.Id;

            foreach (string s in _productRepository.GetDesserts())
            {
                Message Message = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: s,
                        cancellationToken: cancellationToken);
            }

        }        
        public async Task GetSnacks(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var messageText = update.Message.Text;

            var chatId = update.Message.Chat.Id;

            foreach (string s in _productRepository.GetSnacks())
            {
                Message Message = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: s,
                        cancellationToken: cancellationToken);
            }
        }
        public async Task GetDrinks(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var messageText = update.Message.Text;

            var chatId = update.Message.Chat.Id;

            foreach (string s in _productRepository.GetDrinks())
            {
                Message Message = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: s,
                        cancellationToken: cancellationToken);
            }
        }        
    }

}
