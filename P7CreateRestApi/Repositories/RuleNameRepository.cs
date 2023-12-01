using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public class RuleNameRepository : IRuleNameRepository
    {
        private readonly LocalDbContext _context;
        public RuleNameRepository(LocalDbContext context)

        {
            _context = context;
        }

        public async Task<IEnumerable<RuleName>> GetAllAsync()
        {

            return await _context.RuleNames.ToListAsync();
        }
            public async Task<RuleName> GetByIdAsync(int id)
            {
                return await _context.RuleNames.FindAsync(id);
            }

            public async Task AddAsync(RuleName ruleName)
            {
                _context.RuleNames.Add(ruleName);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(RuleName ruleName)
            {
                _context.RuleNames.Update(ruleName);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var ruleName = await _context.RuleNames.FindAsync(id);
                if (ruleName != null)
                {
                    _context.RuleNames.Remove(ruleName);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }

    
