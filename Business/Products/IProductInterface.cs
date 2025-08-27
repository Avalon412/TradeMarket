using Business.Products.DTOs;
using Business.Shared;

namespace Business.Products
{
    public interface IProductInterface : ICrud<ProductReadDto, ProductWriteDto>
    {
    }
}
