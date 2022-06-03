using Telegram.Bot;
using Telegram.Bot.Types;

namespace Core.Interfaces
{
    public interface IAuthentication
    {
        Task UserAuthentication(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
    }
}
