namespace Business.Shared
{
    public interface ICrud<TDtoRead, TDtoWrite>
    {
        Task<IEnumerable<TDtoRead>> GetAllAsync();
        Task<TDtoRead?> GetAsync(int id);
        Task<TDtoRead>AddAsync(TDtoWrite model);
        Task<TDtoRead> UpdateAsync(TDtoWrite model);
        Task DeleteAsync(int id);
    }
}
