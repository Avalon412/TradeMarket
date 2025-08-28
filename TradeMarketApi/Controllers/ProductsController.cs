using Business.Products;
using Business.Products.Commands;
using Business.Products.DTOs;
using Business.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TradeMarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMediator _mediator;

        public ProductsController(IProductService productService, IMediator mediator)
        {
            _productService = productService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAllProducts([FromQuery] FilterSearchDto filter)
        {
            IEnumerable<ProductReadDto> products;

            if (filter.IsEmpty())
            {
                products = await _productService.GetAllAsync();
            }
            else
            {
                var querie = new GetByFilterAsyncQuerie() { Filter =  filter };
                products = await _mediator.Send(querie);
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDto>> GetProductById(int id)
        {
            var product = await _productService.GetAsync(id);

            if (product == null)
            {
                return NotFound(id);
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] ProductWriteDto value)
        {
            await _productService.AddAsync(value);
            var product = await _productService.GetAsync(value.ProductId);

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] ProductWriteDto value)
        {
            await _productService.UpdateAsync(value);
            var updatedProduct = await _productService.GetAsync(id);

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteAsync(id);

            return Ok();
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetAllCategories()
        {
            var categories = await _mediator.Send(new GetAllProductCategoriesAsyncQuerie());

            return Ok(categories);
        }

        [HttpPost("categories")]
        public async Task<ActionResult> AddCategory([FromBody] ProductCategoryDto value)
        {
            await _mediator.Send(new AddCategoryAsyncCommand() { ProductCategoryDto = value });
            var categories = await _mediator.Send(new GetAllProductCategoriesAsyncQuerie());
            var category = categories.FirstOrDefault(x => x.Id == value.Id);

            return Ok(category);
        }

        [HttpDelete("categories/{id}")]
        public async Task<ActionResult> DeleteCategoryProduct(int id)
        {
            await _mediator.Send(new RemoveCategoryAsyncCommand() { CategoryId = id });

            return Ok();
        }
    }
}
