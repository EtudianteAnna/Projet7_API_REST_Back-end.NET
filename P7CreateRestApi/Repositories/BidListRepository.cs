using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Data;

namespace P7CreateRestApi.Repositories
{
    public class BidListRepository : IBidListRepository
    {
        private readonly LocalDbContext _context;

        public BidListRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<BidList> GetByIdAsync(int id)
        {
            return await _context.BidLists.FindAsync(id);
        }

        public async Task AddAsync(BidList bidList)
        {
            _context.BidLists.Add(bidList);
            _ = await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BidList bidList)
        {
            _context.BidLists.Update(bidList);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bidList = await _context.BidLists.FindAsync(id);
            if (bidList != null)
            {
                _context.BidLists.Remove(bidList);
                await _context.SaveChangesAsync();
            }
        }
    }
}
