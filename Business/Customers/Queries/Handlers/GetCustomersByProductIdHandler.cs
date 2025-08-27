using Business.Customers.DTOs;
using DAL.Interfaces;
using MediatR;

namespace Business.Customers.Queries.Handlers
{
    public class GetCustomersByProductIdHandler : IRequestHandler<GetCustomersByProductIdQuerie, IEnumerable<CustomerReadDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomersByProductIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CustomerReadDto>> Handle(GetCustomersByProductIdQuerie request, CancellationToken cancellationToken)
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllWithDetailsAsync();
            var filteredCustomers = customers.Where(c => c.Receipts.Any(r => r.ReceiptDetails.Any(rd => rd.ProductId == request.ProductId)));

            var customersDtos = new List<CustomerReadDto>();

            foreach (var customer in filteredCustomers)
            {
                var customerDto = new CustomerReadDto
                {
                    Id = customer.Id,
                    Name = customer.Person.Name,
                    Surname = customer.Person.Surname,
                    BirthDate = customer.Person.BirthDate,
                    DiscountValue = customer.DiscountValue,
                    ReceiptIds = customer.Receipts.Select(x => x.Id).ToList()
                };

                customersDtos.Add(customerDto);
            }

            return customersDtos;
        }
    }
}
