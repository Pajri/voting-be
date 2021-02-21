using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.Models;

namespace VotingBackend.Repositories
{
    public interface IVotingRepository : IRepository<Voting>
    {
        Task<List<Voting>> GetAllVoting();
        Task<Voting> GetVoteById(Guid id);
        bool IsVotingExists(Guid id);
        Task<Voting> DeleteVoting(Guid id);
        Task<(List<Voting>, int)> Filter(string searchString, Guid selectedCategory, int page);
    }
}
