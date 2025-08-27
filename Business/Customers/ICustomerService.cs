using Business.Customers.DTOs;

namespace Business.Customers
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerReadDto>> GetAllAsync();
        Task<CustomerReadDto> GetAsync(int id);
        Task AddAsync(CustomerWriteDto customer);
        Task UpdateAsync(CustomerWriteDto customer);
        Task DeleteAsync(int customerId);
    }
}
