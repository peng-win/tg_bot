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


    }
}
