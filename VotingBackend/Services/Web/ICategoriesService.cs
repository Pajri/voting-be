using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.ViewModel;

namespace VotingBackend.Services.Web
{
    public interface ICategoriesService
    {
        Task Create(CategoryViewModel category);
        Task Edit(CategoryViewModel category);
        Task<List<CategoryViewModel>> GetAll();
        Task<CategoryViewModel> Detail(Guid id);
        Task<CategoryViewModel> Delete(Guid id);
        bool IsCategoryExists(Guid id);
    }
}
