using Configurations.Processes.Database;
using Configurations.Settings;

namespace Configurations
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDatabaseSettings _databaseSettings;
        private readonly DatabaseProcess _databaseProcess;

        public Worker(ILogger<Worker> logger, IDatabaseSettings databaseSettings)
        {
            _logger = logger;
            _databaseSettings = databaseSettings;
            _databaseProcess = new DatabaseProcess(_logger, _databaseSettings);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Service running at: {time}", DateTimeOffset.Now);

                await _databaseProcess.Initialize();

                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}