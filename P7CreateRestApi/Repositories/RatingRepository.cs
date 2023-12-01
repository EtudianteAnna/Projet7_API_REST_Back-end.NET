using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;


namespace P7CreateRestApi.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly LocalDbContext _ratingContext;
       
        public RatingRepository(LocalDbContext _ratingcontext)
        {
            _ratingContext = _ratingcontext;
        }

        public async Task<IEnumerable<Rating>> GetAllAsync()
        {

            return await _ratingContext.Ratings.ToListAsync();


         }

            public async Task AddAsync(Rating rating)
            {
            _ratingContext.Ratings  .Add(rating);
                await _ratingContext.SaveChangesAsync();
            }

            public async Task UpdateAsync(Rating rating)
            {
                _ratingContext.Ratings.Update(rating);
                await _ratingContext.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var rating = await _ratingContext.Ratings.FindAsync(id);
                if (rating != null)
                {
                    _ratingContext.Ratings.Remove(rating);
                    await _ratingContext.SaveChangesAsync();
                }
            }
        }
    }




