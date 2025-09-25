using Business.Customers.DTOs;
using DAL.Interfaces;
using MediatR;
using System.Linq;

namespace Business.Customers.Queries.Handlers
{
    public class GetCustomerByReceiptIdHandler : IRequestHandler<GetCustomerByReceiptIdQuerie, CustomerReadDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerByReceiptIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerReadDto> Handle(GetCustomerByReceiptIdQuerie request, CancellationToken cancellationToken)
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllWithDetailsAsync();
            var customer = customers.FirstOrDefault(x => x.Receipts.Select(x => x.Id).Contains(request.ReceiptId));

            if (customer is not null)
            {
                return new CustomerReadDto
                {
                    CustomerId = customer.Id,
                    PersonId = customer.PersonId,
                    Name = customer.Person.Name,
                    Surname = customer.Person.Surname,
                    BirthDate = customer.Person.BirthDate,
                    Email = customer.Person.Email,
                    DiscountValue = customer.DiscountValue,
                    ReceiptIds = customer.Receipts.Select(x => x.Id).ToList()
                };
            }

            return new CustomerReadDto { ReceiptIds = new List<int>()};
        }
    }
}
