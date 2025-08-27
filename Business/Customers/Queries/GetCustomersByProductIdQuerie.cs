using Business.Customers.DTOs;
using MediatR;

namespace Business.Customers.Queries
{
    public class GetCustomersByProductIdQuerie : IRequest<IEnumerable<CustomerReadDto>>
    {
        public int ProductId { get; set; }
    }
}
