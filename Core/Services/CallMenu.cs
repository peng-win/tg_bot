using Telegram.Bot;
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
using Telegram.Bot.Types.Enums;

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
            if (messageText.ToLower() == "/menu")
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Выберите пункт меню: ",
                    replyMarkup: keyboard,
                    cancellationToken: cancellationToken);
            }                   

            await SelectMenuItem(botClient, update, cancellationToken);
        }
        
        public async Task SelectMenuItem(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var messageText = update.Message.Text;
            var chatId = update.Message.Chat.Id;

            try
            {
                switch (messageText)
                {
                    case "Пицца":
                        if (Authentication.isAuthorization == false)
                        {
                            await _registration.UserRegistration(botClient, update, cancellationToken);
                        }
                        else await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Выберите товар:",
                                replyMarkup: GetPizza(),
                                cancellationToken: cancellationToken);
                        break;

                    case "Напитки":
                        if (Authentication.isAuthorization == false)
                        {
                            await _registration.UserRegistration(botClient, update, cancellationToken);
                        }
                        else await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Выберите товар:",
                                replyMarkup: GetDrinks(),
                                cancellationToken: cancellationToken);
                        break;

                    case "Десерты":
                        if (Authentication.isAuthorization == false)
                        {
                            await _registration.UserRegistration(botClient, update, cancellationToken);
                        }
                        else await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Выберите товар:",
                                replyMarkup: GetDesserts(),
                                cancellationToken: cancellationToken);
                        break;

                    case "Закуски":
                        if (Authentication.isAuthorization == false)
                        {
                            await _registration.UserRegistration(botClient, update, cancellationToken);
                        }
                        else await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Выберите товар:",
                                replyMarkup: GetSnacks(),
                                cancellationToken: cancellationToken);
                        break;
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }        
        
        private InlineKeyboardMarkup GetDesserts()
        {
            List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
            foreach (string s in _productRepository.GetDesserts())
            {
                buttons.Add(new InlineKeyboardButton(s) { Text = s, CallbackData = $"callback1" });
            }

            var menu = new List<InlineKeyboardButton[]>();
            for (int i = 0; i < buttons.Count; i++)
            {
                menu.Add(new[] { buttons[i] });
            }

            return new InlineKeyboardMarkup(menu.ToArray());

        }
        private InlineKeyboardMarkup GetSnacks()
        {
            List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
            foreach (string s in _productRepository.GetSnacks())
            {
                buttons.Add(new InlineKeyboardButton(s) { Text = s, CallbackData = $"callback1" });
            }

            var menu = new List<InlineKeyboardButton[]>();
            for (int i = 0; i < buttons.Count; i++)
            {
                menu.Add(new[] { buttons[i] });
            }

            return new InlineKeyboardMarkup(menu.ToArray());
        }
        private InlineKeyboardMarkup GetDrinks()
        {
            List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
            foreach (string s in _productRepository.GetDrinks())
            {
                buttons.Add(new InlineKeyboardButton(s) { Text = s, CallbackData = $"callback1" });
            }

            var menu = new List<InlineKeyboardButton[]>();
            for (int i = 0; i < buttons.Count; i++)
            {
                menu.Add(new[] { buttons[i] });
            }

            return new InlineKeyboardMarkup(menu.ToArray());
        }       
        
        private InlineKeyboardMarkup GetPizza()
        {
            List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
            foreach (string s in _productRepository.GetPizza())
            {
                buttons.Add(new InlineKeyboardButton(s) { Text = s, CallbackData = $"callback1" });
            }

            var menu = new List<InlineKeyboardButton[]>();
            for (int i = 0; i < buttons.Count; i++)
            {
                menu.Add(new[] {buttons[i]});
            }

            return new InlineKeyboardMarkup(menu.ToArray());
        }
    }

}
