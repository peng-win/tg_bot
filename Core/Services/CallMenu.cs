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

        //private string[] cart;
        public List<string> cartIds = new List<string>();
        private Guid productId;

        private string callbackvalue;
        private string key;

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
            Console.WriteLine($"Receive message type: {update.Message.Type}");
            if (update.Message.Type != MessageType.Text)
                return;
            var action = update.Message.Text!.Split(' ')[0] switch
            {
                "Пицца" => SendInlineKeyboardPizza(botClient, update.Message),
                "Напитки" => SendInlineKeyboardDrinks(botClient, update.Message),
                "Десерты" => SendInlineKeyboardDesserts(botClient, update.Message),
                "Закуски" => SendInlineKeyboardSnacks(botClient, update.Message),
                "Корзина" => SendСartСontents(botClient, update.Message),
                "/menu" => SendReplyKeyboard(botClient, update.Message),
                "/start" => SendReplyKeyboard(botClient, update.Message),
                _ => Usage(botClient, update.Message)
            };

            async Task SendСartСontents(ITelegramBotClient botClient, Message message)
            {
                decimal sum = 0;
                using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQLConnection")))
                {
                    foreach (string s in cartIds)
                    {
                        InlineKeyboardMarkup keyboardDel = new InlineKeyboardMarkup(new[]
                        {
                            new [] // first row
                            {
                                InlineKeyboardButton.WithCallbackData(text: "Удалить", callbackData: $"callbackdelete:{s}"),
                            }
                        });
                        
                        var price = db.Query<decimal>($"SELECT \"Price\" FROM \"Menu\" WHERE \"Id\" = '{s}'").FirstOrDefault();
                        var name = db.Query<string>($"SELECT \"Product\" FROM \"Menu\" WHERE \"Id\" = '{s}'").FirstOrDefault();

                        sum += price;

                        await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                             text: $"{name} {Math.Round(price)}₽",
                                                             replyMarkup: keyboardDel);
                        
                    }
                    InlineKeyboardMarkup keyboardPay = new InlineKeyboardMarkup(new[]
                        {
                            new [] // first row
                            {
                                InlineKeyboardButton.WithCallbackData(text: "Оплатить", callbackData: $"callbackpay:"),
                            }
                        });
                    await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                             text: $"Сумма: {Math.Round(sum)}₽",
                                                             replyMarkup: keyboardPay);
                }
                if (cartIds.Count == 0)
                {
                    await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                                text: "Корзина пуста");
                }
                
            }            
            async Task<Message> SendInlineKeyboardPizza(ITelegramBotClient botClient, Message message)
            {
                await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                // Simulate longer running task
                await Task.Delay(500);

                List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
                foreach (string s in _productRepository.GetPizza())
                {                    
                    buttons.Add(new InlineKeyboardButton(s) { Text = s, CallbackData = $"callbackproduct:{s}" });                    
                }

                var menu = new List<InlineKeyboardButton[]>();
                for (int i = 0; i < buttons.Count; i++)
                {
                    menu.Add(new[] { buttons[i] });
                }

                InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(menu.ToArray());

                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: "Выберите товар: ",
                                                            replyMarkup: inlineKeyboardMarkup);
            }
            async Task<Message> SendInlineKeyboardDrinks(ITelegramBotClient botClient, Message message)
            {
                await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                // Simulate longer running task
                await Task.Delay(500);

                List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
                foreach (string s in _productRepository.GetDrinks())
                {
                    buttons.Add(new InlineKeyboardButton(s) { Text = s, CallbackData = $"callbackproduct:{s}" });
                }

                var menu = new List<InlineKeyboardButton[]>();
                for (int i = 0; i < buttons.Count; i++)
                {
                    menu.Add(new[] { buttons[i] });
                }

                InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(menu.ToArray());

                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: "Выберите товар: ",
                                                            replyMarkup: inlineKeyboardMarkup);
            }
            async Task<Message> SendInlineKeyboardDesserts(ITelegramBotClient botClient, Message message)
            {
                await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                // Simulate longer running task
                await Task.Delay(500);

                List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
                foreach (string s in _productRepository.GetDesserts())
                {
                    buttons.Add(new InlineKeyboardButton(s) { Text = s, CallbackData = $"callbackproduct:{s}" });
                }

                var menu = new List<InlineKeyboardButton[]>();
                for (int i = 0; i < buttons.Count; i++)
                {
                    menu.Add(new[] { buttons[i] });
                }

                InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(menu.ToArray());

                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: "Выберите товар: ",
                                                            replyMarkup: inlineKeyboardMarkup);
            }
            async Task<Message> SendInlineKeyboardSnacks(ITelegramBotClient botClient, Message message)
            {
                await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                // Simulate longer running task
                await Task.Delay(500);

                List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
                foreach (string s in _productRepository.GetSnacks())
                {
                    buttons.Add(new InlineKeyboardButton(s) { Text = s, CallbackData = $"callbackproduct:{s}" });
                }

                var menu = new List<InlineKeyboardButton[]>();
                for (int i = 0; i < buttons.Count; i++)
                {
                    menu.Add(new[] { buttons[i] });
                }

                InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(menu.ToArray());

                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: "Выберите товар: ",
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
                },
                new[]
                {
                    new KeyboardButton("Корзина")
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
                                     "/start - сделать заказ\n" +
                                     "/menu - вывести меню";

                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: usage,
                                                            replyMarkup: new ReplyKeyboardRemove());
            }            
        }

        private async Task BotOnCallbackQueryReceived(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            int d = callbackQuery.Data.IndexOf(":");
            callbackvalue = callbackQuery.Data.Substring(0, d);
            key = callbackQuery.Data.Substring(d+1);

            //await botClient.AnswerCallbackQueryAsync(
            //    callbackQueryId: callbackQuery.Id,
            //    text: $"{key}");

            switch (callbackvalue)
            {
                case "callbacksize":

                    await GetAsk(botClient, callbackQuery);

                    break;

                case "callbackproduct":
                    await GetMenu(botClient, callbackQuery);
                    break;

                case "callbackyes":
                    cartIds.Add(key.ToString());

                    await botClient.SendTextMessageAsync(
                                    chatId: callbackQuery.Message!.Chat.Id,
                                    text: $"Добавлено в корзину");
                    break;

                case "callbackdelete":
                    cartIds.Remove(key.ToString());
                    await botClient.SendTextMessageAsync(
                                    chatId: callbackQuery.Message!.Chat.Id,
                                    text: $"Товар удален");
                    break;
                //case "callbackpay":
                //    await _registration.UserRegistration(botClient, callbackQuery);
                //    break;                
            }
        }

        async Task GetAsk(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(new[]
            {
                new [] // first row
                {                    
                    InlineKeyboardButton.WithCallbackData(text: "В корзину", callbackData: $"callbackyes:{key}"),
                }
            });

            decimal price = GetPriceProduct().FirstOrDefault();
            string name = GetNameProduct().FirstOrDefault();

            await botClient.SendTextMessageAsync(
                                chatId: callbackQuery.Message!.Chat.Id,
                                text: $"Добавить в корзину? \n\n{name} {Math.Round(price)}₽",                                
                                replyMarkup: keyboard);
        }

        async Task GetMenu(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQLConnection")))
            {
                List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
                foreach (string p in GetProduct(callbackQuery))
                {                    
                    var size = db.Query<string>($"SELECT \"Size\" FROM \"Menu\" WHERE \"Price\" = '{p}'").FirstOrDefault();
                    var weightingrams = db.Query<string>($"SELECT \"WeightInGrams\" FROM \"Menu\" WHERE \"Price\" = '{p}'").FirstOrDefault();
                    var price = db.Query<decimal>($"SELECT \"Price\" FROM \"Menu\" WHERE \"Price\" = '{p}'").FirstOrDefault();                    

                    if (size == null)
                    {
                        productId = db.Query<Guid>($"SELECT \"Id\" FROM \"Menu\" WHERE \"WeightInGrams\" = {weightingrams} AND \"Product\" = '{key}'").FirstOrDefault();
                        buttons.Add(new InlineKeyboardButton("d") { Text = $"{weightingrams}гр. - {Math.Round(price)}₽", CallbackData = $"callbacksize:{productId}" });
                    }
                    else
                    {
                        var unit = db.Query<string>($"SELECT \"Unit\" FROM \"SizeProduct\" WHERE \"Size\" = {size}").FirstOrDefault();
                        productId = db.Query<Guid>($"SELECT \"Id\" FROM \"Menu\" WHERE \"Size\" = {size} AND \"Product\" = '{key}'").FirstOrDefault();

                        buttons.Add(new InlineKeyboardButton("d") { Text = $"{size}{unit} - {Math.Round(price)}₽", CallbackData = $"callbacksize:{productId}" });
                    }
                }

                var menu = new List<InlineKeyboardButton[]>();
                for (int i = 0; i < buttons.Count; i++)
                {
                    menu.Add(new[] { buttons[i] });
                }

                InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(menu.ToArray());

                var prod = key.ToString();
                var description = db.Query<string>($"SELECT \"Description\" FROM \"Products\" WHERE \"Product\" = '{key}'").FirstOrDefault();
                var picture = db.Query<string>($"SELECT \"Picture\" FROM \"Products\" WHERE \"Product\" = '{key}'").FirstOrDefault();

                if (description == null)
                {
                    await botClient.SendPhotoAsync(
                                chatId: callbackQuery.Message!.Chat.Id,
                                photo: picture,
                                caption: $"{prod}",
                                replyMarkup: inlineKeyboardMarkup);

                }
                else await botClient.SendPhotoAsync(
                              chatId: callbackQuery.Message!.Chat.Id,
                              photo: picture,
                              caption: $"{prod} \n\n" +
                                      $"{description}",
                              replyMarkup: inlineKeyboardMarkup);
            }
        }

        public IEnumerable<string> GetProduct(CallbackQuery callbackQuery)
        {
            using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQLConnection")))
            {
                return db.Query<string>($"SELECT \"Price\" FROM \"Menu\" WHERE \"Product\" = '{key}'");
            }
        }

        public IEnumerable<decimal> GetPriceProduct()
        {
            using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQLConnection")))
            {
                return db.Query<decimal>($"SELECT \"Price\" FROM \"Menu\" WHERE \"Id\" = '{key}'");
            }
        }
        public IEnumerable<string> GetNameProduct()
        {
            using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQLConnection")))
            {
                return db.Query<string>($"SELECT \"Product\" FROM \"Menu\" WHERE \"Id\" = '{key}'");
            }
        }
        
        private static Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Update update)
        {
            Console.WriteLine($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }
    }
}
