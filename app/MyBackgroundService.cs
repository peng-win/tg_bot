using Telegram.Bot.Types;
using Telegram.Bot;

namespace app
{
    public class MyBackgroundService : IHostedService, IDisposable
    {
        private readonly ILogger<MyBackgroundService> _logger;
        private int number = 0;
        private Timer timer;

        public MyBackgroundService(ILogger<MyBackgroundService> _logger)
        {
            this._logger = _logger;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(o =>
            {
                Interlocked.Increment(ref number);
                _logger.LogInformation($"Hello {number}");
            },
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stop");
            return Task.CompletedTask;
        }
    }
}
