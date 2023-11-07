using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface ITradeRepository
    {
        Task AddAsync(Trade trade);
        Task UpdateAsync(Trade trade);
        Task DeleteAsync(int tradeId);
        Task<IEnumerable<Trade>> GetAllTradesAsync();//remplacment de List par IEnumerable
        Task GetByIdAsync(int id);
    }
}
