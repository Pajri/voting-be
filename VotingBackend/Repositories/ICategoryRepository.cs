using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.Models;

namespace VotingBackend.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetCategoryById(Guid id);
        Task<Category> DeleteCategory(Guid id);
        bool IsCategoryExists(Guid id);
    }
}
