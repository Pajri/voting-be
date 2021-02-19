using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.Dtos;
using VotingBackend.Models;
using VotingBackend.Repositories;

namespace VotingBackend.Services
{
    public class VotingService : IVotingService
    {
        private readonly IVotingRepository _repository;

        public VotingService(IVotingRepository repository)
        {
            _repository = repository;
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

            var addedVoting = await _repository.AddAsync(newVoting);

            CreateVotingResponseDto response = new CreateVotingResponseDto
            {
                Name = addedVoting.Name,
                Description = addedVoting.Description,
                CreatedDate = addedVoting.CreatedDate,
                DueDate = addedVoting.DueDate,
                VotersCount = addedVoting.VotersCount,
                Category = addedVoting.Category.Name
            };

            return response;
        }

        public async Task<List<Voting>> GetAllVoting()
        {
            return await _repository.GetAllVoting();
        }
    }
}
