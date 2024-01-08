using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task AddUser(User user);
        Task<User> GetUserByCredentialsAsync(string username);
    }
}