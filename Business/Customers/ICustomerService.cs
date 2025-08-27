using Business.Customers.DTOs;
using Business.Shared;

namespace Business.Customers
{
    public interface ICustomerService : ICrud<CustomerReadDto, CustomerWriteDto>
    {
    }
}
