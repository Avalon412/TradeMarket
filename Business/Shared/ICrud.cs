namespace Business.Shared
{
    public interface ICrud<TDtoRead, TDtoWrite>
    {
        Task<IEnumerable<TDtoRead>> GetAllAsync();
        Task<TDtoRead?> GetAsync(int id);
        Task AddAsync(TDtoWrite model);
        Task UpdateAsync(TDtoWrite model);
        Task DeleteAsync(int id);
    }
}
