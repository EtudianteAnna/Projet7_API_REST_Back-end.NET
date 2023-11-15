using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public class RuleNameRepository
    {
        private readonly LocalDbContext _context;

        public RuleNameRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RuleName>> GetAllAsync()
        {
            IQueryable<RuleName> query = _context.RuleNames;

            // Ajouter la vérification pour éviter CS8604
            if (query != null)
            {
                return await query.ToListAsync();
            }
            else
            {
                // Retourner une liste vide ou gérer le cas où la source est nulle selon vos besoins
                return new List<RuleName>();
            }
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
    
