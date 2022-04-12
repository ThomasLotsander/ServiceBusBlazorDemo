
namespace SbSender.Services
{
    public interface IQueueService
    {
        
        Task SendMessageAsync<T>(T servicebBusMessage, string queueName);
    }
}