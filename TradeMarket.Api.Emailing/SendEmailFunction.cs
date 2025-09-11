using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Azure.Communication.Email;
using System.Net;

namespace TradeMarket.Api.Emailing
{
    public class SendEmailFunction
    {
        private readonly EmailClient _emailClient;

        public SendEmailFunction()
        {
            string connectionString = Environment.GetEnvironmentVariable("ACS_CONNECTION_STRING") ?? "";
            _emailClient = new EmailClient(connectionString);
        }

        [Function("SendEmail")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "send-email")] HttpRequestData requestData)
        {
            var emailMessage = new EmailMessage(
                senderAddress: "DoNotReply@b5c48146-ae7a-4a00-9500-7f02d61b156d.azurecomm.net",
                content: new EmailContent("Receipt Checkout")
                {
                    PlainText = "Check is processd successfully"
                },
                recipients: new EmailRecipients(new[]
                {
                    //new EmailAddress("test@example.com")
                    new EmailAddress("marakasi887@gmail.com")
                })
            );

            var op = await _emailClient.SendAsync(Azure.WaitUntil.Completed, emailMessage);

            var response = requestData.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync($"Email status: {op.Value.Status}");
            return response;
        }
    }
}
