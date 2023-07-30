using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Server;
using Server.Settings;
using Services.Infraestructure.Cache;
using Services.Infrastructure.Data;
using System;
using System.IO;
using System.Reflection;

[assembly: FunctionsStartup(typeof(Program))]

namespace Server
{
    internal class Program : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            FunctionsHostBuilderContext context = builder.GetContext();

            if (context.EnvironmentName == EnvironmentSettings.Development)
            {
                builder.ConfigurationBuilder
                       .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), true, false)
                       .AddEnvironmentVariables()
                       .AddUserSecrets(Assembly.GetExecutingAssembly(), true);

                return;
            }

            var vaultSettings = new VaultSettings(context.EnvironmentName);
            if (vaultSettings.Initialized)
            {
                var clientSecretCredential = new ClientSecretCredential(vaultSettings.TenantId, vaultSettings.ClientId, vaultSettings.ClientSecret);
                var secretClient = new SecretClient(new Uri(vaultSettings.KeyVaultAddress), clientSecretCredential);

                builder.ConfigurationBuilder.AddAzureKeyVault(secretClient, new AzureKeyVaultConfigurationOptions());
            }
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddMemoryCache()
                            .AddOptions<ServerSettings>()
                            .Configure<IConfiguration>((settings, configuration) =>
                            {
                                configuration.GetSection(nameof(ServerSettings)).Bind(settings);
                            });

            var serviceProvider = builder.Services.BuildServiceProvider();
            var settings = serviceProvider.GetRequiredService<IOptions<ServerSettings>>().Value;

            builder.Services.AddDbContext<DataContext>(optionsBuilder => optionsBuilder.UseCosmos
            (
                connectionString: settings.ConnectionString,
                databaseName: settings.DatabaseName,
                options =>
                {
                    options.ConnectionMode(ConnectionMode.Direct);
                    options.LimitToEndpoint();
                    options.GatewayModeMaxConnectionLimit(32);
                    options.MaxRequestsPerTcpConnection(4);
                    options.MaxTcpConnectionsPerEndpoint(8);
                    options.OpenTcpConnectionTimeout(TimeSpan.FromMinutes(1));
                    options.RequestTimeout(TimeSpan.FromMinutes(1));
                })
            );

            builder.Services.AddTransient(typeof(Repository));
            builder.Services.AddTransient<ICache, Cache>();            
        }
    }
}