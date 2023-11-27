using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace P7CreateRestApi.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly LocalDbContext _context;
        public RatingRepository _ratingController;

        public RatingRepository(LocalDbContext context)
        {
            _context = context;
        }
       
            public IEnumerable<Rating> GetRatingsAsync()
            {

            return  _context.Rating;
            
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




