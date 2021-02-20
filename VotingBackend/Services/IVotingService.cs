using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.Dtos;
using VotingBackend.Models;

namespace VotingBackend.Services
{
    public interface IVotingService
    {
        Task<List<Voting>> GetAllVoting();
        Task<CreateVotingResponseDto> CreateVoting(CreateVotingRequestDto voting);
        Task<VotingDto> Vote(VoteRequestDto request, Guid userId);
    }
}
