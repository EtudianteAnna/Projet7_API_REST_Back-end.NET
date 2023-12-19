﻿using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using System.Security.Cryptography.X509Certificates;

namespace P7CreateRestApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LocalDbContext _context;

        public UserRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)

            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

                      
        }
        public async Task<User> GetUserByCredentialsAsync(string userName, string password)
        {
            return  await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);

        }
    }
}
