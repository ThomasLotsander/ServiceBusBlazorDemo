using System.Text;
using System.Text.Json;
using Microsoft.Azure.ServiceBus;   

namespace SbSender.Services
{
    public class QueueService : IQueueService
    {

        // Gains access to configuration settings 
        private readonly IConfiguration _config;
        private readonly ILogger<QueueService> _logger;

        public QueueService(IConfiguration config, ILogger<QueueService> logger)
        {
            _config = config;
            _logger = logger;
        }
        
        public async Task SendMessageAsync<T>(T serviceBusMessage, string queueName)
        {
            try
            {
                var cns = _config.GetConnectionString("AzureServiceBus");
                var queueClient = new QueueClient(_config.GetConnectionString("AzureServiceBus"), queueName);
                var messageBody = JsonSerializer.Serialize(serviceBusMessage);
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                await queueClient.SendAsync(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
