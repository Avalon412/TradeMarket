using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Business.Shared.CloudServices
{
    public class AzureMessageQueueService : IMessageQueueService
    {
        public readonly string _connectionString;

        public AzureMessageQueueService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AzureStorage")!;
        }

        public async Task SendMessageAsync<T>(string queueName, T message)
        {
            var client = new QueueClient(_connectionString, queueName);
            await client.CreateIfNotExistsAsync();

            string json = JsonSerializer.Serialize(message);
            string base64message = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            await client.SendMessageAsync(base64message);
        }
    }
}
