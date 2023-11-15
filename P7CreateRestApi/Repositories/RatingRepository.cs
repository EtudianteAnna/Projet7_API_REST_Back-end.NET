using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P7CreateRestApi.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly LocalDbContext _context;

        public RatingRepository(LocalDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Rating>> GetAllAsync()
        {
            IQueryable<Rating> query = _context.Rating;

            // Ajouter la vérification pour éviter CS8604
            if (query != null)
            {
                return await query.ToListAsync();
            }
            else
            {
                // Retourner une liste vide ou gérer le cas où la source est nulle selon vos besoins
                return new List<Rating>();
            }
        }

        public async Task<Rating> GetByIdAsync(int id)
        {
            return await _context.Rating.FindAsync(id);
        }

        public async Task AddAsync(Rating rating)
        {
            _context.Rating.Add(rating);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Rating rating)
        {
            _context.Rating.Update(rating);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var rating = await _context.Rating.FindAsync(id);
            if (rating != null)
            {
                _context.Rating.Remove(rating);
                await _context.SaveChangesAsync();
            }
        }
    }
}



