using Business.Receipts;
using Business.Receipts.Commands;
using Business.Receipts.DTOS;
using Business.Receipts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TradeMarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        private readonly IReceiptService _receiptService;
        private readonly IMediator _mediator;

        public ReceiptsController(IReceiptService receiptService, IMediator mediator)
        {
            _receiptService = receiptService;
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
        public async Task<ActionResult> AddReceipt([FromBody] ReceiptWriteDto value)
        {
            await _receiptService.AddAsync(value);
            var receipt = await _receiptService.GetAsync(value.Id);

            return Ok(receipt);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReceipt(int id, [FromBody] ReceiptWriteDto value)
        {
            await _receiptService.UpdateAsync(value);
            var updatedReceipt = await _receiptService.GetAsync(id);

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
