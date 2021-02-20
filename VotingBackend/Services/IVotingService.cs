using Sieve.Models;
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
        Task<List<VotingDto>> GetAllVoting(SieveModel sieve);
        Task<VotingDto> GetVoteDetail(Guid id);
        Task<CreateVotingResponseDto> CreateVoting(CreateVotingRequestDto voting);
        Task<VotingDto> Vote(VoteRequestDto request, Guid userId);
    }
}
