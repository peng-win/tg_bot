using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Core.Interfaces
{
    public interface ICallMenu
    {
        Task CallMenuTask(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
        Task SelectMenuItem(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
        Task GetPizza(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
        Task GetDrinks(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
        Task GetDesserts(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
        Task GetSnacks(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
    }
}
