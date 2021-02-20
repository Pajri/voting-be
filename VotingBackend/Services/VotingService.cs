using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VotingBackend.Dtos;
using VotingBackend.Exceptions;
using VotingBackend.Models;
using VotingBackend.Repositories;

namespace VotingBackend.Services
{
    public class VotingService : IVotingService
    {
        private readonly IVotingRepository _votingRepository;
        private readonly IUserRepository _userRepository;

        public VotingService(IVotingRepository votingRepository, IUserRepository userRepository)
        {
            _votingRepository = votingRepository;
            _userRepository = userRepository;
        }

        public async Task<CreateVotingResponseDto> CreateVoting(CreateVotingRequestDto voting)
        {
            Voting newVoting = new Voting
            {
                ID = new Guid(),
                Name = voting.Name,
                CreatedDate = DateTime.Now,
                Description = voting.Description,
                DueDate = voting.DueDate,
                CategoryId = voting.CategoryId
            };

            var addedVoting = await _votingRepository.AddAsync(newVoting);

            CreateVotingResponseDto response = new CreateVotingResponseDto
            {
                Name = addedVoting.Name,
                Description = addedVoting.Description,
                CreatedDate = addedVoting.CreatedDate,
                DueDate = addedVoting.DueDate,
                VotersCount = addedVoting.VotersCount
            };

            return response;
        }

        public async Task<List<Voting>> GetAllVoting()
        {
            return await _votingRepository.GetAllVoting();
        }

        public async Task<VotingDto> Vote(VoteRequestDto request, Guid userId)
        {
            try
            {
                var vote = await _votingRepository.GetVoteById(request.VoteID);

                if (vote.Voters != null && vote.Voters.Any(v => v.ID == userId))
                {
                    throw new HttpExceptionBase("User already voted");
                }

                if (vote.DueDate < DateTime.Now)
                {
                    throw new HttpExceptionBase("Vote has passed due date");
                }

                if (vote.Voters == null) vote.Voters = new List<User>();

                var user = await _userRepository.GetUserById(userId);
                vote.Voters.Add(user);
                vote.VotersCount++;

                await _votingRepository.UpdateAsync(vote);

                return new VotingDto
                {
                    ID = vote.ID,
                    Name = vote.Name,
                    Description = vote.Description,
                    DueDate = vote.DueDate,
                    CreatedDate = vote.CreatedDate,
                    VotersCount = vote.VotersCount
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
