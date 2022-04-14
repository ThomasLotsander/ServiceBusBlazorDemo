// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.Json;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using SbShared.Models;

namespace SbReceiver
{
    class Program
    {
        const string queueName = "personqueue";
        static IQueueClient queueClient;

        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);
            var _config = builder.Build();

            var test = _config.GetConnectionString("AzureServiceBus");

            queueClient = new QueueClient(_config.GetConnectionString("AzureServiceBus"), queueName);
            var messageHandlerOption = new MessageHandlerOptions(ExceptionReceivedContext)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOption);
            Console.ReadLine();
            await queueClient.CloseAsync();

        }

        private static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var jsonString = Encoding.UTF8.GetString(message.Body);
            var person = JsonSerializer.Deserialize<PersonModel>(jsonString);
            Console.WriteLine($"Person received: {person.FirstName} {person.LastName}");

            await queueClient.CompleteAsync(message.SystemProperties.LockToken); 
        }

        private static Task ExceptionReceivedContext(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Message handler Exception: { arg.Exception }");
            return Task.CompletedTask;
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<Program>();
        }
    }
}
