using Business.Customers.DTOs;
using MediatR;

namespace Business.Customers.Queries
{
    public class GetCustomerByReceiptIdQuerie : IRequest<CustomerReadDto>
    {
        public int ReceiptId { get; set; }
    }
}
