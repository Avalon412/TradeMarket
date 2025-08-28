using Business.Receipts.DTOS;
using DAL.Entities;
using DAL.Interfaces;

namespace Business.Receipts
{
    public class ReceiptService : IReceiptService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReceiptService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReceiptReadDto>> GetAllAsync()
        {
            var receipts = await _unitOfWork.ReceiptRepository.GetAllWithDetailsAsync();

            var receiptDtos = new List<ReceiptReadDto>();

            foreach (var receipt in receipts)
            {
                var receiptDto = new ReceiptReadDto
                {
                    Id = receipt.Id,
                    CustomerId = receipt.CustomerId,
                    IsCheckedOut = receipt.IsCheckedOut,
                    OperationDate = receipt.OperationDate,
                    ReceiptDetailsIds = receipt.ReceiptDetails.Select(x => x.Id).ToList(),
                };

                receiptDtos.Add(receiptDto);
            }

            return receiptDtos;
        }

        public async Task<ReceiptReadDto?> GetAsync(int id)
        {
            var receipt = await _unitOfWork.ReceiptRepository.GetByIdWithDetailsAsync(id);

            if (receipt is not null)
            {
                return new ReceiptReadDto
                {
                    Id = receipt.Id,
                    CustomerId = receipt.CustomerId,
                    IsCheckedOut = receipt.IsCheckedOut,
                    OperationDate = receipt.OperationDate,
                    ReceiptDetailsIds = receipt.ReceiptDetails.Select(x => x.Id).ToList(),
                };
            }

            return new ReceiptReadDto { ReceiptDetailsIds = new List<int>() };
        }

        public async Task AddAsync(ReceiptWriteDto model)
        {
            var entity = new Receipt
            {
                CustomerId = model.CustomerId,
                IsCheckedOut = model.IsCheckedOut,
                OperationDate = model.OperationDate,
            };

            _unitOfWork.ReceiptRepository.Add(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(ReceiptWriteDto model)
        {
            var entity = new Receipt
            {
                Id= model.Id,
                CustomerId = model.CustomerId,
                IsCheckedOut = model.IsCheckedOut,
                OperationDate = model.OperationDate,
            };

            _unitOfWork.ReceiptRepository.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var receipt = await _unitOfWork.ReceiptRepository.GetByIdWithDetailsAsync(id);

            if (receipt != null)
            {
                foreach (var item in receipt.ReceiptDetails)
                {
                    _unitOfWork.ReceiptDetailRepository.Delete(item);
                }

                await _unitOfWork.ReceiptRepository.DeleteByIdAsync(id);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task<decimal> ToPay(int receiptId)
        {
            var receipt = await _unitOfWork.ReceiptRepository.GetByIdWithDetailsAsync(receiptId);

            return receipt!.ReceiptDetails.Sum(x => x.Quantity * x.DiscountUnitPrice);
        }

        public async Task CheckOutAsync(int receiptId)
        {
            var receipt = await _unitOfWork.ReceiptRepository.GetByIdAsync(receiptId);
            if (receipt != null)
            {
                receipt.IsCheckedOut = true;
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
