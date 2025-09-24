namespace Business.Shared
{
    public interface IMessageQueueService
    {
        Task SendMessageAsync<T>(string queueName, T message);
    }
}
