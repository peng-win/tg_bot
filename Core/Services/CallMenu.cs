using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
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

        public CallMenu(IConfiguration configuration, IProductRepository productRepository)
        {
            _configuration = configuration;
            _productRepository = productRepository;
        }
        public async Task CallMenuTask(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;
            try
            {
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
                switch (messageText)
                {
                    case "Пицца":                        
                        await GetPizza(botClient, update, cancellationToken);                        
                        break;

                    case "Напитки":
                        await GetDrinks(botClient, update, cancellationToken);
                        break;

                    case "Десерты":
                        await GetDesserts(botClient, update, cancellationToken);
                        break;

                    case "Закуски":
                        await GetSnacks(botClient, update, cancellationToken);
                        break;

                    default:
                        await GetAllProducts(botClient, update, cancellationToken);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }

        }
        public async Task GetAllProducts(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var messageText = update.Message.Text;

            var chatId = update.Message.Chat.Id;

            foreach (string s in _productRepository.GetAllProducts())
            {
                Message Message = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: s,
                        cancellationToken: cancellationToken);
            }
            
        }
        public async Task GetPizza(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var messageText = update.Message.Text;

            var chatId = update.Message.Chat.Id;
            /*
            string size = _productRepository.GetSizePizza().FirstOrDefault();
            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: size, callbackData: "11"),
                },
            });*/

            foreach (string s in _productRepository.GetPizza())
            {
                Message Message = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: s,
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
        public InlineKeyboardMarkup GenerateButtonsList(string arg)
        {
            var ikbList = new List<InlineKeyboardButton[]>();
            foreach (string s in _productRepository.GetPizza())
            {
                var ikb = new List<InlineKeyboardButton>();

                foreach (string size in _productRepository.GetSizePizza())
                {
                    ikb.Add(new InlineKeyboardButton(text: size));
                }
                ikbList.Add(ikb.ToArray());
            }

            return new InlineKeyboardMarkup(ikbList.ToArray());
        }
    }

}
