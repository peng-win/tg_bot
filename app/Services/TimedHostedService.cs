using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.InlineQueryResults;
using Npgsql;
using Core.Interfaces;
using Dapper;

namespace app.Services
{  
    public class TimedHostedService : IHostedService
    {
        private readonly ILogger<TimedHostedService> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICallMenu _callMenu;
        private readonly IRegistration _registration;

        public TimedHostedService(ILogger<TimedHostedService> logger, IConfiguration configuration, ICallMenu callMenu, IRegistration registration)
        {
            _registration = registration;
            _callMenu = callMenu; 
            _logger = logger;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");
          
            var token = _configuration.GetValue<string>("token");

            var bot = new TelegramBotClient(token);

            using var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions { AllowedUpdates = { } };

            bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken: cts.Token);

            return Task.CompletedTask;
        }
        
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                //await _authentication.UserAuthentication(botClient, update, cancellationToken);
                
                 await _callMenu.CallMenuTask(botClient, update, cancellationToken);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }        

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(errorMessage);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Hosted Service is stopping.");            

            return Task.CompletedTask;
        }        
    }    
}
