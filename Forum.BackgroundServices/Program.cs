using BackGroundServices;
using BackGroundServices.BackGroundWorkers;
using Forum.Application.MainTopics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        try
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .ConfigureServices((hostContext, services) =>
            {
                HttpClientConfiguration.ConfigureHttpClient(services);
                services.AddSingleton<ITopicService, TopicService>();
                services.AddHostedService<TopicWorker>();
            });
}