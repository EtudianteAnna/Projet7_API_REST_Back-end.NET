using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface IBidListRepository
    {
        Task<IEnumerable<Rating>> GetAllAsync();
        Task<Rating> GetByIdAsync(int id);
        Task AddAsync(Rating rating);
        Task UpdateAsync(Rating rating);
        Task DeleteAsync(int id);
    }
}