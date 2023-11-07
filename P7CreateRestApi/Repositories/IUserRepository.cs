using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Controllers;

namespace Dot.Net.WebApi.Repositories
{
    public interface IUserRepository
    {
        User FindByUserName(string userName);
        Task<List<User>> FindAll();
        void Add(User user);
        void Update(User user);
        Task SaveChangesAsync();
        public class UserRepository : IUserRepository
        {
            private readonly LocalDbContext _dbContext;

            public UserRepository(LocalDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public User FindByUserName(string userName)
            {
                if (_dbContext == null)
                {
                    throw new ArgumentNullException(nameof(_dbContext), "LocalDbContext is not initialized.");
                }


                return _dbContext.Users.FirstOrDefault(user => user.Username == userName);
            }

            public async Task<List<User>> FindAll()
            {
                return await _dbContext.Users.ToListAsync();
            }

            public void Add(User user)
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                _dbContext.Users.Add(user);
            }

            public void Update(User user)
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                _dbContext.Entry(user).State = EntityState.Modified;
            }

            public async Task SaveChangesAsync()
            {
                await _dbContext.SaveChangesAsync();
            }

            internal object FindByUserName(object name)
            {
                throw new NotImplementedException();
            }
        }
    }
}
