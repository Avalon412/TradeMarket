using Business.Customers.Queries;
using Business.Receipts;
using Business.Receipts.Commands;
using Business.Receipts.DTOS;
using Business.Receipts.Queries;
using Business.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TradeMarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        private readonly IReceiptService _receiptService;
        private readonly IMessageQueueService _messageQueueService;
        private readonly IMediator _mediator;

        public ReceiptsController(IReceiptService receiptService, IMessageQueueService messageQueueService, IMediator mediator)
        {
            _receiptService = receiptService;
            _messageQueueService = messageQueueService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptReadDto>>> GetAllReceipt()
        {
            var receipts = await _receiptService.GetAllAsync();

            return Ok(receipts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptReadDto>> GetReceiptById(int id)
        {
            var receipt = await _receiptService.GetAsync(id);

            if (receipt == null)
            {
                return NotFound(id);
            }

            return Ok(receipt);
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<IEnumerable<ReceiptDetailDto>>> GetReceiptDetailsById(int id)
        {
            var receiptDetails = await _mediator.Send(new GetReceiptDetailsAsyncQuerie() { ReceiptId = id });

            if (receiptDetails == null)
            {
                return NotFound(id);
            }

            return Ok(receiptDetails);
        }

        [HttpGet("{id}/sum")]
        public async Task<ActionResult<decimal>> GetReceiptDetailsSum(int id)
        {
            var receiptDetails = await _mediator.Send(new GetReceiptDetailsAsyncQuerie() { ReceiptId = id });

            if (receiptDetails == null)
            {
                return NotFound(id);
            }

            var sum = receiptDetails.Sum(x => x.DiscountUnitPrice * x.Quantity);

            return Ok(sum);
        }

        [HttpGet("period")]
        public async Task<ActionResult<IEnumerable<ReceiptReadDto>>> GetReceiptsByPeriod([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            if (startDate == null || endDate == null)
                return BadRequest();

            var receipts = await _mediator.Send(new GetReceiptByPeriodQuerie() { StartDate = startDate.Value, EndDate = endDate.Value });

            return Ok(receipts);
        }

        [HttpPost]
        public async Task<ActionResult<ReceiptReadDto>> AddReceipt([FromBody] ReceiptWriteDto value)
        {
            var receipt = await _receiptService.AddAsync(value);

            return Ok(receipt);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReceiptReadDto>> UpdateReceipt(int id, [FromBody] ReceiptWriteDto value)
        {
            var updatedReceipt = await _receiptService.UpdateAsync(value);

            return Ok(updatedReceipt);
        }

        [HttpPut("{id}/products/add/{productId}/{quantity}")]
        public async Task<ActionResult> AddProductToReceipt(int id, int productId, int quantity)
        {
            await _mediator.Send(new AddProductToReceiptAsyncCommand() { ReceiptId = id, ProductId = productId, Quantity = quantity });
            var updatedReceipt = await _mediator.Send(new GetReceiptDetailsAsyncQuerie() { ReceiptId = id });
            var updatedReceiptDetail = updatedReceipt.FirstOrDefault(x => x.ProductId == productId);

            return Ok(updatedReceiptDetail);
        }

        [HttpPut("{id}/products/remove/{productId}/{quantity}")]
        public async Task<ActionResult> RemoveProductFromReceipt(int id, int productId, int quantity)
        {
            await _mediator.Send(new RemoveProductFromReceiptAsyncCommand() { ReceiptId = id, ProductId = productId, Quantity = quantity });

            return Ok();
        }

        [HttpPut("{id}/checkout")]
        public async Task<ActionResult> Checkout(int id)
        {
            await _receiptService.CheckOutAsync(id);

            var customer = await _mediator.Send(new GetCustomerByReceiptIdQuerie() { ReceiptId = id });

            await _messageQueueService.SendMessageAsync("checkout-emails", new { ReceiptId = id, customer.Email });

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReceipt(int id)
        {
            await _receiptService.DeleteAsync(id);

            return Ok();
        }
    }
}
