using Business.Products.DTOs;
using Business.Statistics;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TradeMarketApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _statisticService;
        private readonly IMediator _mediator;

        public StatisticsController(IStatisticService statisticService, IMediator mediator)
        {
            _statisticService = statisticService;
            _mediator = mediator;
        }

        [HttpGet("popularProducts")]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetPopularProducts([FromQuery] int productCount)
        {
            var popularProducts = await _statisticService.GetMostPopularProductsAsync(productCount);

            return Ok(popularProducts);
        }

        [HttpGet("customer/{id}/{productCount}")]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetNumberOfFavoritProducts(int id, int productCount)
        {
            var popularProducts = await _statisticService.GetCustomersMostPopularProductsAsync(id, productCount);

            return Ok(popularProducts);
        }

        [HttpGet("activity/{customerCount}")]
        public async Task<ActionResult<IEnumerable<CustomerActivityDto>>> GetCustomersActivity(int customerCount, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var customersActivity = await _statisticService.GetMostValuableCustomersAsync(customerCount, startDate, endDate);

            return Ok(customersActivity);
        }

        [HttpGet("income/{categoryId}")]
        public async Task<ActionResult<decimal>> GetIncomeByCategory(int categoryId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var income = await _statisticService.GetIncomeOfCategoryInPeriod(categoryId, startDate, endDate);

            return Ok(income);
        }
    }
}
