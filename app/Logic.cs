namespace app
{
    public class Logic : ILogic
    {
        private readonly ILogger<Logic> logger;
        private int number = 0;

        public Logic(ILogger<Logic> logger)
        {
            this.logger = logger;
        }
        public async Task DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Interlocked.Increment(ref number);
                logger.LogInformation($"Startup {number}");
                await Task.Delay(1000 * 5);
            }
        }
    }
}
