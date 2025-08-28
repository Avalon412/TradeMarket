using Business.Products.DTOs;
using Business.Shared;

namespace Business.Products
{
    public interface IProductService : ICrud<ProductReadDto, ProductWriteDto>
    {
    }
}
