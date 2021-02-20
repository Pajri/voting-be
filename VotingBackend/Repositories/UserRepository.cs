using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.Data;
using VotingBackend.Models;

namespace VotingBackend.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(VotingDbContext context) : base(context)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await GetAll().Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmailAndRole(string email, string role)
        {
            return await GetAll().Where(u => u.Email == email && u.Role == role).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await GetAll().Where(u => u.ID == id).FirstOrDefaultAsync();
        }
    }
}
