using P7CreateRestApi.Domain;

public interface IRatingRepository
{
    IEnumerable<Rating> GetRatingsAsync();
    Task AddAsync(Rating rating);
    Task UpdateAsync(Rating rating);
    Task DeleteAsync(int id);
}
