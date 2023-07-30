using Configurations.Settings;
using Microsoft.EntityFrameworkCore;
using Services.Infrastructure.Data;

namespace Configurations.Processes.Database
{
    public class DatabaseProcess
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDatabaseSettings _databaseSettings;

        public DatabaseProcess(ILogger<Worker> logger, IDatabaseSettings databaseSettings)
        {
            _logger = logger;
            _databaseSettings = databaseSettings;
        }

        public async Task Initialize()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                    .UseCosmos
                    (
                        _databaseSettings.Host,
                        _databaseSettings.Key,
                        databaseName: _databaseSettings.Name
                    )
                    .Options;

            using var context = new DataContext(options);

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            _logger.LogInformation("Database Initialized: {time}", DateTimeOffset.Now);

            await Load(context);
        }

        private async Task Load(DataContext dataContext)
        {
            _logger.LogInformation("Loading Data: {time}", DateTimeOffset.Now);

            await dataContext.Informations.AddRangeAsync(Loads.Informations.Get());
            await dataContext.Partners.AddRangeAsync(Loads.Partners.Get());

            await dataContext.SaveChangesAsync();

            _logger.LogInformation("Finish Load: {time}", DateTimeOffset.Now);
        }
    }
}