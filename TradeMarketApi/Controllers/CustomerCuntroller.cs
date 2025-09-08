using Business.Customers;
using Business.Customers.DTOs;
using Business.Customers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TradeMarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerCuntroller : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMediator _mediator;

        public CustomerCuntroller(ICustomerService customerService, IMediator mediator)
        {
            _customerService = customerService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerReadDto>>> Get()
        {
            var response = await _customerService.GetAllAsync();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerReadDto>> GetById(int id)
        {
            var customer = await _customerService.GetAsync(id);

            if (customer == null)
            {
                return NotFound(id);
            }

            return Ok(customer);
        }

        [HttpGet("products/{id}")]
        public async Task<ActionResult<IEnumerable<CustomerReadDto>>> GetByProductId(int id)
        {
            var querie = new GetCustomersByProductIdQuerie() { ProductId = id };
            var customers = await _mediator.Send(querie);

            if (customers == null)
            {
                return NotFound(id);
            }

            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerReadDto>> Add([FromBody] CustomerWriteDto value)
        {
            var customer = await _customerService.AddAsync(value);

            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerReadDto>> Update(int Id, [FromBody] CustomerWriteDto value)
        {
            var updatedCustomer = await _customerService.UpdateAsync(value);

            return Ok(updatedCustomer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _customerService.DeleteAsync(id);

            return Ok();
        }
    }
}
