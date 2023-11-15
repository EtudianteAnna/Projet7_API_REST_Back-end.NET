using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public class TradeRepository
    {
        private readonly LocalDbContext _context;

        public TradeRepository(LocalDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Trade>> GetAllAsync()
        {
            IQueryable<Trade> query = _context.Trades;

            // Ajouter la vérification pour éviter CS8604
            if (query != null)
            {
                return await query.ToListAsync();
            }
            else
            {
                // Retourner une liste vide ou gérer le cas où la source est nulle selon vos besoins
                return new List<Trade>();
            }
        }

        public async Task<Trade> GetByIdAsync(int id)
        {
            return await _context.Trades.FindAsync(id);
        }

        public async Task AddAsync(Trade trade)
        {
            _context.Trades.Add(trade);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Trade trade)
        {
            _context.Trades.Update(trade);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var trade = await _context.Trades.FindAsync(id);
            if (trade != null)
            {
                _context.Trades.Remove(trade);
                await _context.SaveChangesAsync();
            }
        }
    }
}