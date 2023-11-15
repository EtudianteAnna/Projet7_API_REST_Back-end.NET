using P7CreateRestApi.Domain;

public interface IRatingRepository
{
    Task<IEnumerable<Rating>> GetAllAsync();
    Task<Rating> GetByIdAsync(int id);
    Task AddAsync(Rating rating);
    Task UpdateAsync(Rating rating);
    Task DeleteAsync(int id);
}
