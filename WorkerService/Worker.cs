namespace WorkerService
{
    /*
     *  Standard services are designed to run as a long-running process that listens for requests from clients and responds to them.
     *  They typically expose an API that clients can use to interact with the service.

     *  Worker services, on the other hand, are designed to run continuously in the background,
     *  separate from the main application thread. They are not intended to be directly exposed to clients,
     *  but instead are used to perform background tasks, such as data processing, file monitoring, or network communication.
     */

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}