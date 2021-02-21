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
        public async Task<Voting> GetVoteById(Guid id)
        {
            return await GetAll().Where(v => v.ID == id)
                    .Include(v => v.Category)
                    .Include(v => v.Voters)
                    .FirstOrDefaultAsync();
        }

        public async Task<Voting> DeleteVoting(Guid id)
        {
            Voting voting;
            voting = await GetVoteById(id);
            _context.Remove(voting);
            await _context.SaveChangesAsync();
            return voting;
        }

        public async Task<(List<Voting>,int)> Filter(string searchString, Guid selectedCategory, int page)
        {
            IQueryable<Voting> votings = GetAll().Include(v => v.Category);
            if (searchString != null && searchString != "")
            {
                votings = votings.Where(v => v.Name.ToLower().Contains(searchString.ToLower()));
            }

            if (selectedCategory != Guid.Empty)
            {
                votings = votings.Where(v => v.Category.ID == selectedCategory);
            }

            int pageSize = 5;
            int total = votings.Count();
            List<Voting> votingList = await votings.Skip(page * pageSize).Take(pageSize).ToListAsync();

            return (votingList, (int) Math.Ceiling((double) total/pageSize));
        }

        public bool IsVotingExists(Guid id)
        {
            return GetAll().Any(v => v.ID == id);
        }
    }
}
