using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.ViewModel;

namespace VotingBackend.Services.Web
{
    public interface IVotingsService
    {
        Task Create(VotingViewModel voting);
        Task<VotingViewModel> Details(Guid id);
        Task Edit(VotingViewModel voting);
        bool IsVotingExists(Guid id);
        Task Delete(Guid id);
        Task<(List<VotingViewModel>,int)> Filter(string searchString, Guid selectedCategory, int page);
    }
}
