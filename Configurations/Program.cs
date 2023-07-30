using Configurations;
using Configurations.Settings;
using Microsoft.Extensions.Options;

IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.Configure<DatabaseSettings>(hostContext.Configuration.GetSection("Database"));
                services.AddSingleton<IDatabaseSettings>(x => x.GetRequiredService<IOptions<DatabaseSettings>>().Value);
                services.AddHostedService<Worker>();
            })
            .Build();

await host.RunAsync();
