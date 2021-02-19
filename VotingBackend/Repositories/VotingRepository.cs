using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.Data;
using VotingBackend.Models;

namespace VotingBackend.Repositories
{
    public class VotingRepository : Repository<Voting>, IVotingRepository
    {
        public VotingRepository(VotingDbContext context) : base(context)
        {
        }

        public async Task<List<Voting>> GetAllVoting()
        {
            return await GetAll().ToListAsync();
        }
    }
}
