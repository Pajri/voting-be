using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.Dtos;
using VotingBackend.Models;
using VotingBackend.Repositories;

namespace VotingBackend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateCategoryResponseDto> CreateCategory(CreateCategoryRequestDto request)
        {
            Category newCategory = new Category
            {
                ID = new Guid(),
                Name = request.Name
            };

            Category createdCategory;
            CreateCategoryResponseDto response;
            try
            {
                createdCategory = await _repository.AddAsync(newCategory);
                response = new CreateCategoryResponseDto
                {
                    ID = createdCategory.ID,
                    Name = createdCategory.Name
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        public async Task<UpdateCategoryResponseDto> UpdateCategory(UpdateCategoryRequestDto request)
        {
            try
            {
                Category category;
                category = await _repository.GetCategoryById(request.ID);
                category.Name = request.Name;

                category = await _repository.UpdateAsync(category);

                UpdateCategoryResponseDto response = new UpdateCategoryResponseDto()
                {
                    ID = category.ID,
                    Name = category.Name
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CategoryDto> DeleteCategory(Guid id)
        {
            try
            {
                var category = await _repository.DeleteCategory(id);

                CategoryDto response = new CategoryDto()
                {
                    ID = category.ID,
                    Name = category.Name
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CategoryDto>> GetAllCategory()
        {
            try
            {
                var categoryList = await _repository.GetAll().ToListAsync();
                List<CategoryDto> resultList = new List<CategoryDto>();
                foreach (var item in categoryList)
                {

                    resultList.Add(new CategoryDto {ID=item.ID, Name=item.Name});
                }

                return resultList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
