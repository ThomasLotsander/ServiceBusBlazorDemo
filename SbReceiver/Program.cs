// See https://aka.ms/new-console-template for more information
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace SbReceiver
{
    class Program
    {
        const string connectionString = "Endpoint=sb://tlotsander.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=C+2CNoCGg+Z4OZY4ZcVdjnCybuVgyvwPDVDin9YIqHI=";
        const string queueName = "personqueue";
        static IQueueClient queueClient;

        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);
            var _config = builder.Build();
            var connection = _config.GetConnectionString("AzureServiceBus");
            queueClient = new QueueClient(connectionString, queueName);
            var messageHandlerOption = new MessageHandlerOptions(ExceptionReceivedContext)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOption);

            Console.ReadLine();
            await queueClient.CloseAsync();

        }

        private static Task ProcessMessagesAsync(Message arg1, CancellationToken arg2)
        {
            throw new NotImplementedException();
        }

        private static Task ExceptionReceivedContext(ExceptionReceivedEventArgs arg)
        {
            throw new NotImplementedException();
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<Program>();
        }
    }
}
