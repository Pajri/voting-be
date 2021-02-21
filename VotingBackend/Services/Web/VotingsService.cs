using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.Models;
using VotingBackend.Repositories;
using VotingBackend.ViewModel;

namespace VotingBackend.Services.Web
{
    public class VotingsService : IVotingsService
    {
        private readonly IVotingRepository _votingRepository;
        private readonly ICategoryRepository _categoryRepository;

        public VotingsService(IVotingRepository votingRepository, ICategoryRepository categoryRepository)
        {
            _votingRepository = votingRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task Create(VotingViewModel voting)
        {
            var category = await _categoryRepository.GetCategoryById(voting.CategoryId);
            var _voting = new Voting
            {
                ID = new Guid(),
                Name = voting.Name,
                Description = voting.Description,
                CategoryId = voting.CategoryId,
                DueDate = voting.DueDate,
                VotersCount = 0,
                CreatedDate = DateTime.Now,
                Category = category
            };

            await _votingRepository.AddAsync(_voting);
        }

        public async Task<VotingViewModel> Details(Guid id)
        {
            var voting = await _votingRepository.GetVoteById(id);

            if (voting == null) return null;

            return new VotingViewModel
            {
                Name = voting.Name,
                Description = voting.Description,
                DueDate = voting.DueDate,
                Category = voting.Category.Name,
                CreatedDate = voting.CreatedDate,
                VotersCount = voting.VotersCount,
                CategoryId = voting.CategoryId
            };
        }

        public async Task Edit(VotingViewModel voting)
        {
            var _voting = await _votingRepository.GetVoteById(voting.ID);
            _voting.Name = voting.Name;
            _voting.Description = voting.Description;
            _voting.CategoryId = voting.CategoryId;
            _voting.DueDate = voting.DueDate;

            await _votingRepository.UpdateAsync(_voting);
        }

        public async Task<(List<VotingViewModel>,int)> Filter(string searchString, Guid selectedCategory, int page)
        {
            var (votings,pageNum) = await _votingRepository.Filter(searchString, selectedCategory, page);
            List<VotingViewModel> votingList = new List<VotingViewModel>();
            foreach (var voting in votings)
            {
                votingList.Add(new VotingViewModel
                {
                    ID = voting.ID,
                    Category = voting.Category.Name,
                    Name = voting.Name,
                    CreatedDate = voting.CreatedDate,
                    Description = voting.Description,
                    DueDate = voting.DueDate,
                    VotersCount = voting.VotersCount
                });
            }

            return (votingList, pageNum);
        }

        public async Task Delete(Guid id)
        {
            await _votingRepository.DeleteVoting(id);
        }

        public bool IsVotingExists(Guid id)
        {
            return _votingRepository.IsVotingExists(id);
        }
    }
}
