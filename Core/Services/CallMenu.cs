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
            var handler = update.Type switch
            {                
                UpdateType.Message => BotOnMessageReceived(botClient, update, cancellationToken),
                UpdateType.CallbackQuery => BotOnCallbackQueryReceived(botClient, update.CallbackQuery!),
                _ => UnknownUpdateHandlerAsync(botClient, update)
            };

            try
            {
                await handler;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
        private async Task BotOnMessageReceived(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var messageText = update.Message.Text;
            var chatId = update.Message.Chat.Id;
            Console.WriteLine($"Receive message type: {update.Message.Type}");
            if (update.Message.Type != MessageType.Text)
                return;
            var action = update.Message.Text!.Split(' ')[0] switch
            {
                "Пицца" => SendInlineKeyboard(botClient, update.Message),
                "Напитки" => SendInlineKeyboard(botClient, update.Message),
                "Десерты" => SendInlineKeyboard(botClient, update.Message),
                "Закуски" => SendInlineKeyboard(botClient, update.Message),
                "/menu" => SendReplyKeyboard(botClient, update.Message),
                _ => Usage(botClient, update.Message)
            };

            static async Task<Message> SendInlineKeyboard(ITelegramBotClient botClient, Message message)
            {
                await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                // Simulate longer running task
                await Task.Delay(500);

                List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
                buttons.Add(new InlineKeyboardButton("d") { Text = "d", CallbackData = $"call" });
                var menu = new List<InlineKeyboardButton[]>();
                for (int i = 0; i < buttons.Count; i++)
                {
                    menu.Add(new[] { buttons[i] });
                }

                InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(menu.ToArray());

                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: "Choose",
                                                            replyMarkup: inlineKeyboardMarkup);
            }

            static async Task<Message> SendReplyKeyboard(ITelegramBotClient botClient, Message message)
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
                
                 return await botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Выберите пункт меню: ",
                        replyMarkup: keyboard);
                
            }

            static async Task<Message> Usage(ITelegramBotClient botClient, Message message)
            {
                const string usage = "Usage:\n" +
                                     "/inline   - send inline keyboard\n" +
                                     "/keyboard - send custom keyboard\n" +
                                     "/remove   - remove custom keyboard\n" +
                                     "/photo    - send a photo\n" +
                                     "/request  - request location or contact";

                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: usage,
                                                            replyMarkup: new ReplyKeyboardRemove());
            }

            switch (messageText)
            {/*
                case "Пицца":                    
                    if (Authentication.isAuthorization == false)
                    {
                        await _registration.UserRegistration(botClient, update, cancellationToken);
                    }
                    else await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Выберите товар:",
                            replyMarkup: GetSomeKeyboard(),
                            cancellationToken: cancellationToken);
                    break;
                */
            }
        }

        private static async Task BotOnCallbackQueryReceived(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            await botClient.AnswerCallbackQueryAsync(
                callbackQueryId: callbackQuery.Id,
                text: $"Received {callbackQuery.Data}");

            await botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                text: $"Received {callbackQuery.Data}");
        }

        private InlineKeyboardMarkup GetSomeKeyboard()
        {
            List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
            buttons.Add(new InlineKeyboardButton("d") { Text = "d", CallbackData = $"call"});
            var menu = new List<InlineKeyboardButton[]>();
            for (int i = 0; i < buttons.Count; i++)
            {
                menu.Add(new[] { buttons[i] });
            }

            return new InlineKeyboardMarkup(menu.ToArray());
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
                buttons.Add(new InlineKeyboardButton(s) { Text = s, CallbackData = $"{s}" });
            }

            var menu = new List<InlineKeyboardButton[]>();
            for (int i = 0; i < buttons.Count; i++)
            {
                menu.Add(new[] {buttons[i]});
            }

            return new InlineKeyboardMarkup(menu.ToArray());
        }
        private static Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Update update)
        {
            Console.WriteLine($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }
    }

}
