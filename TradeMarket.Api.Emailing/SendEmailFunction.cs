using System.Text.Json;
using Azure.Communication.Email;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TradeMarket.Api.Emailing
{
    public class SendEmailFunction
    {
        private readonly EmailClient _emailClient;
        private readonly ILogger _logger;

        public SendEmailFunction(ILogger<SendEmailFunction> logger)
        {
            string connectionString = Environment.GetEnvironmentVariable("ACS_CONNECTION_STRING") ?? "";
            _emailClient = new EmailClient(connectionString);
            _logger = logger;
        }

        [Function("SendEmail")]
        public async Task Run(
            [QueueTrigger("checkout-emails", Connection = "AzureWebJobsStorage")] string queueMessage)
        {
            var message = JsonSerializer.Deserialize<CheckOutMessage>(queueMessage);

            var emailMessage = new EmailMessage(
                senderAddress: "DoNotReply@b5c48146-ae7a-4a00-9500-7f02d61b156d.azurecomm.net",
                content: new EmailContent("Receipt Checkout")
                {
                    PlainText = $"Check is processd successfully. Check id - {message?.ReceiptId}"
                },
                recipients: new EmailRecipients(new[]
                {
                    //new EmailAddress("test@example.com")
                    new EmailAddress("marakasi887@gmail.com")
                })
            );

            var op = await _emailClient.SendAsync(Azure.WaitUntil.Completed, emailMessage);

            _logger.LogInformation($"Email sent. Status: {op.Value.Status}");
        }

        private class CheckOutMessage
        {
            public int ReceiptId { get; set; }
        }
    }
}
