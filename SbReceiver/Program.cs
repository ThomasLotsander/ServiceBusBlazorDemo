// See https://aka.ms/new-console-template for more information
using Microsoft.Azure.ServiceBus;

namespace SbReceiver
{
    class Program
    {
        const string connectionString = "";
        const string queueName = "personqueue";
        static IQueueClient queueClient;

        static async Task Main(string[] args)
        {
            queueClient = new QueueClient(connectionString, queueName);
        }
    }
}
