using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.Models;
using VotingBackend.Repositories;
using VotingBackend.ViewModel;

namespace VotingBackend.Services.Web
{ 
    public class CategoriesService : ICategoriesService
    {
        ICategoryRepository _repository;
        public CategoriesService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task Create(CategoryViewModel category)
        {
            Category newCategory = new Category
            {
                ID = new Guid(),
                Name = category.Name,
            };

            await _repository.AddAsync(newCategory);
        }

        public async Task<List<CategoryViewModel>> GetAll()
        {
            var categories = await _repository.GetAll().ToListAsync();

            List<CategoryViewModel> categoryList = new List<CategoryViewModel>();
            foreach (var item in categories)
            {
                categoryList.Add(new CategoryViewModel()
                {
                    ID = item.ID,
                    Name = item.Name
                });
            }

            return categoryList;
        }

        public async Task<CategoryViewModel> Detail(Guid id)
        {
            var category = await _repository.GetCategoryById(id);

            var _category = new CategoryViewModel
            {
                ID = category.ID,
                Name = category.Name
            };

            return _category;
        }

        public async Task Edit(CategoryViewModel category)
        {
            var _category = await _repository.GetCategoryById(category.ID);
            if (_category != null)
            {
                _category.Name = category.Name;
            }

            await _repository.UpdateAsync(_category);
        }

        public async Task<CategoryViewModel> Delete(Guid id)
        {
            var category = await _repository.DeleteCategory(id);
            return new CategoryViewModel { ID = category.ID, Name = category.Name};
        }
        
        public bool IsCategoryExists(Guid id)
        {
            return _repository.IsCategoryExists(id);
        }
    }
}