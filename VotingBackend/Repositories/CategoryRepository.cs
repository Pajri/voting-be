using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.Data;
using VotingBackend.Models;

namespace VotingBackend.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        
        public CategoryRepository(VotingDbContext context) : base(context)
        {
        }

        public async Task<Category> GetCategoryById(Guid id)
        {
            Category category;
            category = await GetAll().Where(c => c.ID == id).FirstOrDefaultAsync();
            return category;
        }

        public async Task<Category> DeleteCategory(Guid id)
        {
            Category category;
            category = await GetCategoryById(id);
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public bool IsCategoryExists(Guid id)
        {
            return GetAll().Any(c => c.ID == id);
        }
    }
}
