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
    }
}
