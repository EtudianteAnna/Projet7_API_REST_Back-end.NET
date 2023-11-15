using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public class UserRepository  
    {
        private readonly LocalDbContext _context;

        public UserRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            IQueryable<User> query = _context.User;

            // Ajouter la vérification pour éviter CS8604
            if (query != null)
            {
                return await query.ToListAsync();
            }
            else
            {
                // Retourner une liste vide ou gérer le cas où la source est nulle selon vos besoins
                return new List<User>();
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task AddAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _ = _context.User.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var rating = await _context.User.FindAsync(id);
            if (rating != null)
            {
                _context.User.Remove(rating);
                await _context.SaveChangesAsync();
            }
        }
    }
}
