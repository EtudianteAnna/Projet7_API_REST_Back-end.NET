using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface IRuleNameRepository
    {

        Task<IEnumerable<RuleName>> GetAllAsync();
        Task<RuleName> GetByIdAsync(int id);
        Task AddAsync(RuleName ruleName);
        Task UpdateAsync(RuleName ruleName);
        Task DeleteAsync(int id);
    }
}