using Business.Customers.DTOs;
using DAL.Entities;
using DAL.Interfaces;

namespace Business.Customers
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CustomerReadDto>> GetAllAsync()
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllWithDetailsAsync();

            var customersDtos = new List<CustomerReadDto>();

            foreach (var customer in customers)
            {
                var customerDto = new CustomerReadDto
                {
                    CustomerId = customer.Id,
                    PersonId = customer.PersonId,
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

        public async Task<CustomerReadDto?> GetAsync(int id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdWithDetailsAsync(id);

            if (customer is not null)
            {
                return new CustomerReadDto
                {
                    CustomerId = customer.Id,
                    PersonId = customer.PersonId,
                    Name = customer.Person.Name,
                    Surname = customer.Person.Surname,
                    BirthDate = customer.Person.BirthDate,
                    DiscountValue = customer.DiscountValue,
                    ReceiptIds = customer.Receipts.Select(x => x.Id).ToList()
                };
            }

            return new CustomerReadDto();
        }

        public async Task AddAsync(CustomerWriteDto customer)
        {
            var entity = new Customer
            {
                Person = new Person { Name = customer.Name, Surname = customer.Surname, BirthDate = customer.BirthDate },
                DiscountValue = customer.DiscountValue
            };

            _unitOfWork.CustomerRepository.Add(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(CustomerWriteDto customer)
        {
            var entity = new Customer
            {
                Id = customer.CustomerId,
                Person = new Person { Id = customer.PersonId, Name = customer.Name, Surname = customer.Surname, BirthDate = customer.BirthDate },
                DiscountValue = customer.DiscountValue
            };

            _unitOfWork.CustomerRepository.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdWithDetailsAsync(id);

            await _unitOfWork.CustomerRepository.DeleteByIdAsync(customer!.Id);
            await _unitOfWork.PersonRepository.DeleteByIdAsync(customer!.PersonId);
            await _unitOfWork.SaveAsync();
        }
    }
}
